using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryRestaurante
    {
        IEnumerable<Restaurante> GetRestaurante();
        Restaurante GetRestauranteByID(int id);
    }
}
