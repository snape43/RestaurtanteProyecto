using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEstadoMesa : IServiceEstadoMesa
    {
        public IEnumerable<EstadoMesa> GetEstadoMesa()
        {
            IRepositoryEstadoMesa repository = new RepositoryEstadoMesa();
            return (IEnumerable<EstadoMesa>)repository.GetEstadoMesa();
        }
    }
}
