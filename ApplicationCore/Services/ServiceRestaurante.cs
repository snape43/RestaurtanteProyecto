using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRestaurante : IServiceRestaurante
    {
        public IEnumerable<Restaurante> GetRestaurante()
        {
            IRepositoryRestaurante repository = new RepositoryRestaurante();
            return repository.GetRestaurante();
        }

        public Restaurante GetRestauranteByID(int id)
        {
            IRepositoryRestaurante repository = new RepositoryRestaurante();
            return repository.GetRestauranteByID(id);
        }
    }
}
