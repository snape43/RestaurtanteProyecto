using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryPedido : IRepositoryPedido
    {
        public IEnumerable<Pedido> GetPedido()
        {
            try
            {

                IEnumerable<Pedido> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Pedido
                    lista = ctx.Pedido.Include("Mesa.Restaurante").Include("EstadoPedido").ToList();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Pedido GetPedidoByID(int id)
        {
            Pedido oPedido = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                oPedido = ctx.Pedido.
                    Where(l => l.Id == id).
                    Include("Usuario").Include("Usuario1").Include("DetallePedido").Include("Mesa")
                    .Include("EstadoPedido").Include("DetallePedido.Producto").Include("Mesa.Restaurante").
                    FirstOrDefault();

            }
            return oPedido;
        }
    }
}

