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
        public void GetPedidoCountDate(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var resultado = ctx.Pedido.GroupBy(x => x.FechaPedido).
                               Select(o => new {
                                   Count = o.Count(),
                                   FechaPedido = o.Key
                               });
                    foreach (var item in resultado)
                    {
                        varEtiquetas += String.Format("{0:dd/MM/yyyy}", item.FechaPedido) + ",";
                        varValores += item.Count + ",";
                    }
                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
                throw new Exception(mensaje);
            }
        }
        public Pedido Save(Pedido pPedido)
        {
            int resultado = 0;
            Pedido pedido = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Pedido
                // 2- DetallePedido
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.Pedido.Add(pPedido);
                        resultado = ctx.SaveChanges();
                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    pedido = GetPedidoByID(pPedido.Id);


                return pedido;
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
                throw new Exception(mensaje);
            }
        }
    }
}

