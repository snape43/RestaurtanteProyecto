using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);
        Producto Save(Producto producto);

    }
}
