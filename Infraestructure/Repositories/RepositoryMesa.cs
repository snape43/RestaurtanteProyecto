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
    public class RepositoryMesa : IRepositoryMesa
    {
        public IEnumerable<Mesa> GetMesa()
        {
            try
            {

                IEnumerable<Mesa> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Mesa
                    lista = ctx.Mesa.Include("EstadoMesa").Include("Restaurante").ToList();

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

        public Mesa GetMesaByID(int id)
        {
            Mesa oMesa = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                oMesa = ctx.Mesa.
                    Where(l => l.Id == id).
                    Include("Restaurante").Include("EstadoMesa").Include("Restaurante.Producto.DetallePedido.Pedido").
                    FirstOrDefault();

            }
            return oMesa;
        }

        public Mesa Save(Mesa mesa, string[] selectedRestaurantes)
        {
            int retorno = 0;
            Mesa oMesa = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oMesa = GetMesaByID((int)mesa.Id);
                IRepositoryRestaurante _RepositoryRestaurante = new RepositoryRestaurante();

                if (oMesa == null)
                {

                    //Insertar
                    //Logica para agregar las Restaurantes al Mesa
                    if (selectedRestaurantes != null)
                    {

                       // mesa.Restaurante = new List<Restaurante>();
                        foreach (var Restaurante in selectedRestaurantes)
                        {
                            var RestauranteToAdd = _RepositoryRestaurante.GetRestauranteByID(int.Parse(Restaurante));
                            ctx.Restaurante.Attach(RestauranteToAdd); //sin esto, EF intentará crear una categoría
                         //   mesa.Restaurante.Add(RestauranteToAdd);// asociar a la categoría existente con el Mesa


                        }
                    }
                    //Insertar Mesa
                    ctx.Mesa.Add(mesa);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Mesa
                    ctx.Mesa.Add(mesa);
                    ctx.Entry(mesa).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    //Logica para actualizar Restaurantes
                    var selectedRestaurantesID = new HashSet<string>(selectedRestaurantes);
                    if (selectedRestaurantes != null)
                    {
                      //  ctx.Entry(mesa).Collection(p => p.).Load();
                        var newRestauranteForMesa = ctx.Restaurante
                         .Where(x => selectedRestaurantesID.Contains(x.Id.ToString())).ToList();
                       // mesa.Restaurante = newRestauranteForMesa;

                        ctx.Entry(mesa).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oMesa = GetMesaByID((int)mesa.Id);

            return oMesa;
        }
    
    }
}
