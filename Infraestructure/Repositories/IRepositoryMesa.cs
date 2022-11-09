using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepositoryMesa
    {
        IEnumerable<Mesa> GetMesa();
        Mesa GetMesaByID(int id);

        Mesa Save(Mesa mesa, string[] selectedCategorias);
    }
}
