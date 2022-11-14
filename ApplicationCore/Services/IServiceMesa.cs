using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceMesa
    {
        IEnumerable<Mesa> GetMesa();
        Mesa GetMesaByID(int id);
        IEnumerable<string> GetMesaNumero();
        Mesa Save(Mesa mesa);
    }
}
