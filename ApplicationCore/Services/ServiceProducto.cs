using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public IEnumerable<Producto> GetProducto()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }

        public Producto GetProductoByID(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }

        public IEnumerable<string> GetProductoNombres()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto().Select(x => x.Descripcion);
        }
        public Producto Save(Producto producto, string[] selectedCategorias)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.Save(producto, selectedCategorias);
        }

    }
}
