// SistemaDeLogistica.Tests/ReportTests.cs
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using DomainModel.Entities;
using DomainModel.ValueObjects;
using Repositorio.Implementations;
using DomainModel.Interfaces;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using BusinessLayer.DTOs;

public class ReportTests
{
    private readonly ITransferenciaService _service;

    public ReportTests()
    {
        // 1) Creamos repositorios en memoria y el servicio
        var ubicRepo = new UbicacionRepository();
        var prodRepo = new ProductoRepository();
        var userRepo = new UsuarioRepository();
        var movRepo = new MovimientoHistoricoRepository();
        _service = new TransferenciaService(ubicRepo, userRepo, prodRepo, movRepo);

        // 2) Seed: ubicaciones, productos, usuarios
        var dir = new Direccion("Calle", "1", "Loc", "0000", "Prov");
        var dep = new Deposito(1, "Depósito Central", dir);
        var tie = new Tienda(2, "Tienda Norte", dir);
        ubicRepo.Add(dep);
        ubicRepo.Add(tie);

        var p1 = new Producto(1, "P1", "Desc", 10m);
        var p2 = new Producto(2, "P2", "Desc", 20m);
        prodRepo.Add(p1);
        prodRepo.Add(p2);

        var u1 = new Usuario(1, "User1");
        var u2 = new Usuario(2, "User2");
        userRepo.Add(u1);
        userRepo.Add(u2);

        // 3) Seed de stock en depósito para evitar excepciones
        dep.AumentarStock(p1, 100);
        dep.AumentarStock(p2, 100);

        // 4) Función auxiliar para crear movimientos
        void AddMov(int hour, Usuario u, Ubicacion origen, Ubicacion destino, Producto prod, int qty)
        {
            _service.RealizarTransferencia(new TransferRequest
            {
                OrigenId = origen.UbicacionId,
                DestinoId = destino.UbicacionId,
                UsuarioId = u.UsuarioId,
                Detalles = new List<DetalleTransferDto> {
                    new DetalleTransferDto { ProductoId = prod.ProductoId, Cantidad = qty }
                }
            });
        }

        // 5) Crear 5 transferencias (4 a tienda, 1 de retorno)
        AddMov(9, u1, dep, tie, p1, 5);
        AddMov(12, u2, dep, tie, p2, 3);
        AddMov(15, u1, dep, tie, p1, 2);
        AddMov(18, u2, dep, tie, p2, 1);
        AddMov(20, u1, tie, dep, p1, 4);

        // 6) Ajustar manualmente las fechas en el repositorio
        var all = movRepo.GetAll().ToList();
        for (int i = 0; i < all.Count; i++)
        {
            all[i].Fecha = new DateTime(2025, 5, 20, 9 + i * 3, 0, 0);
        }
    }

    [Fact]
    public void Informe_A_FiltrarPorDestino_OrdenDescendente()
    {
        // a) destino = tienda (2)
        var resultados = _service.GetMovimientosPorDestino(2).ToList();

        // Debe devolver exactamente las 4 transferencias a la tienda
        Assert.Equal(4, resultados.Count);
        // La primera debe ser la de hora más alta (18)
        Assert.Equal(18, resultados.First().Fecha.Hour);
        // Verificar campos
        Assert.All(resultados, m =>
        {
            Assert.NotNull(m.Origen);
            Assert.NotNull(m.Usuario);
            Assert.True(m.Detalles.Count > 0);
            Assert.InRange(m.Fecha.Hour, 9, 18);
        });
    }

    [Fact]
    public void Informe_B_DestinosConMasDeTresOperaciones_EnUnDia()
    {
        // b) destinos que superen 3 ops el 20/05/2025
        var día = new DateTime(2025, 5, 20);
        var lista = _service
            .GetMovimientosDestinosConMasDe(3, día)
            .ToList();

        // Solo la tienda (2) hizo 4 operaciones ese día
        Assert.All(lista, m => Assert.Equal(2, m.Destino.UbicacionId));
        Assert.Equal(4, lista.Count);
        // Verificar orden ascendente
        Assert.True(lista.SequenceEqual(lista.OrderBy(m => m.Fecha)));
    }

    [Fact]
    public void Informe_C_Primeros10Movimientos_PorTienda_EntreFechas()
    {
        // c) primeros 10 entre 19/05/2025 y 21/05/2025 para tienda (2)
        var desde = new DateTime(2025, 5, 19);
        var hasta = new DateTime(2025, 5, 21);
        var movs = _service
            .GetMovimientosTiendaRango(2, desde, hasta, 10)
            .ToList();

        // Solo devolverá las 4 transferencias a la tienda
        Assert.Equal(4, movs.Count);
        Assert.All(movs, m =>
        {
            Assert.Equal(2, m.Destino.UbicacionId);
            Assert.InRange(m.Fecha, desde, hasta);
        });
    }
}
