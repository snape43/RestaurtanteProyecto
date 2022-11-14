using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {


        // GET: Producto
        public ActionResult Index()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _SeviceProducto = new ServiceProducto();
                lista = _SeviceProducto.GetProducto();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto oProducto = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oProducto = _ServiceProducto.GetProductoByID(Convert.ToInt32(id));
                if (oProducto == null)
                {
                    TempData["Message"] = "No existe el Producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(oProducto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();
                lista = _ServiceProducto.GetProducto();
                //Lista autocompletado de CategoriaProductoes
                ViewBag.listaNombres = _ServiceProducto.GetProductoNombres();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        //listar todas las categorias un get
        private SelectList listaCategorias(int id = 0)
        {
            IServiceCategoriaProducto _ServiceCategoriaProducto = new ServiceCategoriaProducto();
            IEnumerable<CategoriaProducto> listaCategoriaProductos = _ServiceCategoriaProducto.GetCategoriaProducto();
            return new SelectList(listaCategoriaProductos, "Id","NombreCategoria",id);
        }

        private MultiSelectList listaCategoriasByID(ICollection<CategoriaProducto> categoriaById = null)
        {
            IServiceCategoriaProducto _ServiceCategoria = new ServiceCategoriaProducto();
            IEnumerable<CategoriaProducto> listaCategorias = _ServiceCategoria.GetCategoriaProducto();
            //Selecionar los CaterogiaProductos / Modificar
            int[] listaCaterogiaProductosSelect = null;
            if (categoriaById != null)
            {
                listaCaterogiaProductosSelect = categoriaById.Select(c => c.Id).ToArray();
            }

            return new MultiSelectList(listaCategorias, "Id", "NombreCategoria", listaCaterogiaProductosSelect);
        }


        private MultiSelectList listaRestaurantes(ICollection<Restaurante> restaurantes = null)
        {
            IServiceRestaurante _ServiceCategoria = new ServiceRestaurante();
            IEnumerable<Restaurante> listaRestauarantes = _ServiceCategoria.GetRestaurante();
           //Selecionar los restaurantes / Modificar
            int[] listaRestaurantesSelect = null;
          if (restaurantes != null)
           {
            listaRestaurantesSelect = restaurantes.Select(c => c.Id).ToArray();
           }

           return new MultiSelectList(listaRestauarantes, "Id", "Nombre","Direccion","Telefono", listaRestaurantesSelect);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            //Que recursos necesito para crear un libro

            //CategoriaProducto
            ViewBag.IdCategoriaProducto = listaCategoriasByID();
            ViewBag.IdCategoria = listaCategorias();
            //Categorías

            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Save(Producto producto, HttpPostedFileBase ImageFile)
        {

            MemoryStream target = new MemoryStream();
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (producto.Foto == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        producto.Foto = target.ToArray();
                        ModelState.Remove("Foto");
                    }
                }
                if (ModelState.IsValid)
                {
                    Producto oProductoI = _ServiceProducto.Save(producto);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
               //     Utils.Util.ValidateErrors(this);
                    ViewBag.IdCategoria = listaCategorias(producto.IdCategoriaProducto);
                    return View("Create", producto);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            // Que recursos necesito para actualizar un Producto
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                //Producto 
                producto = _ServiceProducto.GetProductoByID(Convert.ToInt32(id));
                if (producto == null)
                {
                    TempData["Message"] = "No existe el Producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                //CategoriaProducto
                //ViewBag.IdRestaurantes = listaRestaurantes(Producto.IdCategoriaProducto);

                //Categorías
                ViewBag.IdCategoria = listaCategorias(producto.IdCategoriaProducto);
                return View(producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
            
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
