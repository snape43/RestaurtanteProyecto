using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRestaurante
    {
        IEnumerable<Restaurante> GetRestaurante();
        Restaurante GetRestauranteByID(int id);
    }
}
