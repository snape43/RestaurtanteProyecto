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
    public class RepositoryProducto : IRepositoryProducto
    {
        public IEnumerable<Producto> GetProducto()
        {
            try
            {

                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Producto
                    lista = ctx.Producto.Include("CategoriaProducto").Include("Restaurante").ToList();

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

        public Producto GetProductoByID(int id)
        {
            Producto oProducto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                oProducto = ctx.Producto.
                    Where(l => l.Id == id).
                    Include("CategoriaProducto").
                    FirstOrDefault();

            }
            return oProducto;
        }

        public Producto Save(Producto producto)
        {
            int retorno = 0;
            Producto oProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = GetProductoByID((int)producto.Id);
              //  IRepositoryCategoriaProducto _RepositoryCategoriaProducto = new RepositoryCategoriaProducto();

                if (oProducto == null)
                {

                    //Insertar
                    //Logica para agregar las categorias de los productos
                  /*  if (selectedCategorias != null)
                    {
                        //Todo:duda
                        // producto.CategoriaProducto = new List<CategoriaProducto>();
                        foreach (var categoria in selectedCategorias)
                        {
                         //   var categoriaToAdd = _RepositoryCategoriaProducto.GetCategoriaProductoByID(int.Parse(categoria));
                            ctx.CategoriaProducto.Attach(categoriaToAdd); //sin esto, EF intentará crear una categoría
                            //producto.CategoriaProducto.Add(categoriaToAdd);// asociar a la categoría existente con el libro


                        }
                    }*/
                    //Insertar a producto
                    ctx.Producto.Add(producto);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                  //  retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Libro
                 //   ctx.Producto.Add(producto);
                    ctx.Entry(producto).State = EntityState.Modified;
                   // retorno = ctx.SaveChanges();

                    //Logica para actualizar Categorias
                 //   var selectedCategoriasID = new HashSet<string>(selectedCategorias);
                    //TODO: collectiones categorias de los productos
                    //if (selectedCategorias != null)
                    //{
                    //    ctx.Entry(producto).Collection(p => p.CategoriaProducto).Load();
                    //    var newCategoriaForProducto = ctx.CategoriaProducto
                    //     .Where(x => selectedCategoriasID.Contains(x.NombreCategoria.ToString())).ToList();
                    //    producto.CategoriaProducto = newCategoriaForProducto;

                    //    ctx.Entry(producto).State = EntityState.Modified;
                    //    retorno = ctx.SaveChanges();
                    //}
                }
                retorno = ctx.SaveChanges();
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)producto.Id);

            return oProducto;
        }
    }
}

