using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessLayer; // Namespace donde está tu extensión AddBusinessLayer
using DomainModel.Interfaces;
using DomainModel.Entities;
using DomainModel.ValueObjects;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar BusinessLayer (repositorios + servicios)
builder.Services.AddBusinessLayer();

// 2. Registrar controladores y Swagger/OpenAPI
builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
     {
         // Ignorar ciclos de referencia
         opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
         // (Opcional) JSON más legible
         opts.JsonSerializerOptions.WriteIndented = true;
     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var ubicRepo = scope.ServiceProvider.GetRequiredService<IUbicacionRepository>();
    var prodRepo = scope.ServiceProvider.GetRequiredService<IProductoRepository>();
    var userRepo = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();

    // 1. Dirección base
    var direccion = new Direccion(
        calle: "Av. Siempre Viva",
        numero: "742",
        localidad: "Springfield",
        codigoPostal: "12345",
        provincia: "Provincia X"
    );

    // 2. Depósito y Tienda
    var deposito = new Deposito(1, "Depósito Central", direccion);
    var tienda = new Tienda(2, "Tienda Norte", direccion);
    ubicRepo.Add(deposito);
    ubicRepo.Add(tienda);

    // 3. Productos
    var producto1 = new Producto(1, "Producto A", "Descripción A", 100m);
    var producto2 = new Producto(2, "Producto B", "Descripción B", 200m);
    prodRepo.Add(producto1);
    prodRepo.Add(producto2);

    // 4. Usuarios
    var admin = new Usuario(1, "admin");
    var operador = new Usuario(2, "operador");
    userRepo.Add(admin);
    userRepo.Add(operador);

    // 5. Stock inicial en el depósito
    deposito.AumentarStock(producto1, 50);
    deposito.AumentarStock(producto2, 30);
}
// 3. Pipeline de peticiones
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();