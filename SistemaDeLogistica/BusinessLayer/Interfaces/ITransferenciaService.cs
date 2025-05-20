using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using BusinessLayer.DTOs;


namespace BusinessLayer.Interfaces
{
    public interface ITransferenciaService
    {
        MovimientoHistorico RealizarTransferencia(TransferRequest request);
        MovimientoHistorico GetMovimiento(int id);
        // otros métodos según los informes que necesites

        IEnumerable<MovimientoHistorico> GetAllMovimientos();

        // Informe a) Movimientos por destino, ordenados por fecha descendente
        IEnumerable<MovimientoHistorico> GetMovimientosPorDestino(int destinoId);

        // Informe b) Movimientos en un día con más de N operaciones por destino, orden ascendente
        IEnumerable<MovimientoHistorico> GetMovimientosDestinosConMasDe(int minOperaciones, DateTime día);

        // Informe c) Primeros X movimientos de una tienda en un rango de fechas
        IEnumerable<MovimientoHistorico> GetMovimientosTiendaRango(int tiendaId, DateTime desde, DateTime hasta, int maxResults);

    }
}
