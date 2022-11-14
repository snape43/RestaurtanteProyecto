using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceMesa : IServiceMesa
    {
        public IEnumerable<Mesa> GetMesa()
        {
            IRepositoryMesa repository = new RepositoryMesa();
            return repository.GetMesa();
        }

        public Mesa GetMesaByID(int id)
        {
            IRepositoryMesa repository = new RepositoryMesa();
            return repository.GetMesaByID(id);
        }
        public IEnumerable<string> GetMesaNumero()
        {
            IRepositoryMesa repository = new RepositoryMesa();
            return repository.GetMesa().Select(x => x.NumeroMesa);
        }

        public Mesa Save(Mesa mesa)
        {
            IRepositoryMesa repository = new RepositoryMesa();
            return repository.Save(mesa);
        }
    }
}
