using Infraestructure.Models;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePedido : IServicePedido
    {


        public IEnumerable<Pedido> GetPedido()
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.GetPedido();
        }

        public Pedido GetPedidoByID(int id)
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.GetPedidoByID(id);
        }
        public void GetPedidoCountDate(out string etiquetas1, out string valores1)
        {
            IRepositoryPedido repository = new RepositoryPedido();

            repository.GetPedidoCountDate(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public Pedido Save(Pedido pedido)
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.Save(pedido);
        }
    }
}

