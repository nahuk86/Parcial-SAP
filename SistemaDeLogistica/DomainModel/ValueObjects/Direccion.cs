using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ValueObjects
{
    public class Direccion
    {
        public string Calle { get; private set; }
        public string Numero { get; private set; }
        public string Localidad { get; private set; }
        public string CodigoPostal { get; private set; }
        public string Provincia { get; private set; }

        public Direccion(string calle, string numero, string localidad, string codigoPostal, string provincia)
        {
            Calle = calle;
            Numero = numero;
            Localidad = localidad;
            CodigoPostal = codigoPostal;
            Provincia = provincia;
        }
    }
}
