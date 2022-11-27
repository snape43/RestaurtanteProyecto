using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePedido
    {
        IEnumerable<Pedido> GetPedido();
        Pedido GetPedidoByID(int id);
        Pedido Save(Pedido pedido);

        void GetPedidoCountDate(out string etiquetas, out string valores);

    }
}
