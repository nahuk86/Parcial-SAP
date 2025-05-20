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
    }
}
