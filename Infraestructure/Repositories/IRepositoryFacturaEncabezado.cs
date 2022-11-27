using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryFacturaEncabezado
    {
        IEnumerable<FacturaEncabezado> GetFacturaEncabezado();
        FacturaEncabezado GetFacturaEncabezadoByID(int id);
        FacturaEncabezado Save(FacturaEncabezado facturaEncabezado);
    }
}
