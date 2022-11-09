using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoriaProducto : IServiceCategoriaProducto
    {
        public IEnumerable<CategoriaProducto> GetCategoriaProducto()
        {
            IRepositoryCategoriaProducto repository = new RepositoryCategoriaProducto();
            return repository.GetCategoriaProducto();
        }

        public CategoriaProducto GetCategoriaProductoByID(int id)
        {
            IRepositoryCategoriaProducto repository = new RepositoryCategoriaProducto();
            return repository.GetCategoriaProductoByID(id);
        }
    }
}
