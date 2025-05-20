using BusinessLayer.DTOs;
using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SitioWeb.Services
{
    public class TransferenciaApiClient
    {
        private readonly HttpClient _httpClient;

        public TransferenciaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtiene todas las ubicaciones (depósitos y tiendas).
        /// </summary>
        public Task<List<Ubicacion>> GetUbicacionesAsync()
            => _httpClient.GetFromJsonAsync<List<Ubicacion>>("api/ubicaciones");

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        public Task<List<Producto>> GetProductosAsync()
            => _httpClient.GetFromJsonAsync<List<Producto>>("api/productos");

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        public Task<List<Usuario>> GetUsuariosAsync()
            => _httpClient.GetFromJsonAsync<List<Usuario>>("api/usuarios");

        /// <summary>
        /// Crea una nueva transferencia.
        /// </summary>
        public async Task<MovimientoHistorico> CreateTransferenciaAsync(TransferRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/transferencias", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MovimientoHistorico>();
        }

        /// <summary>
        /// Obtiene el detalle de una transferencia por ID.
        /// </summary>
        public Task<MovimientoHistorico> GetMovimientoAsync(int id)
            => _httpClient.GetFromJsonAsync<MovimientoHistorico>($"api/transferencias/{id}");
    }
}
