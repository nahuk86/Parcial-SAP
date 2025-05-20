using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; private set; }
        public string Nombre { get; private set; }

        public Usuario(int usuarioId, string nombre)
        {
            UsuarioId = usuarioId;
            Nombre = nombre;
        }
    }
}
