using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DomainModel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio.Implementations;

namespace BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            // Repositorios en memoria como SINGLETON para conservar el seed
            services.AddSingleton<IUbicacionRepository, UbicacionRepository>();
            services.AddSingleton<IProductoRepository, ProductoRepository>();
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IMovimientoHistoricoRepository, MovimientoHistoricoRepository>();

            services.AddScoped<IUbicacionService, UbicacionService>();
            services.AddScoped<ITransferenciaService, TransferenciaService>();

            return services;
        }
    }
}
