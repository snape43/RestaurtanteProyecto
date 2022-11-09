using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryCategoriaProducto
    {
        IEnumerable<CategoriaProducto> GetCategoriaProducto();
        CategoriaProducto GetCategoriaProductoByID(int id);
    }
}
