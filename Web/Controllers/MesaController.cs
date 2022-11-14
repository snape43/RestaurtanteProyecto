using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MesaController : Controller
    {
        // GET: Mesa
        public ActionResult Index()
        {
            IEnumerable<Mesa> lista = null;
            try
            {
                IServiceMesa _SeviceMesa = new ServiceMesa();
                lista = _SeviceMesa.GetMesa();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        public ActionResult IndexAdmin()
        {
            IEnumerable<Mesa> lista = null;
            try
            {
                IServiceMesa _ServiceMesa = new ServiceMesa();
                lista = _ServiceMesa.GetMesa();
                //Lista autocompletado de CategoriaMesaes
                ViewBag.listaNombres = _ServiceMesa.GetMesaNumero();
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
        // GET: Mesa/Details/5
        public ActionResult Details(int? id)
        {
            ServiceMesa _ServiceMesa = new ServiceMesa();
            Mesa Mesa = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                Mesa = _ServiceMesa.GetMesaByID(Convert.ToInt32(id));
                if (Mesa == null)
                {
                    TempData["Message"] = "No existe el Mesa solicitado";
                    TempData["Redirect"] = "Mesa";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(Mesa);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Mesa";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaRestaurante(int idRestaurante = 0)
        {
            IServiceRestaurante _ServiceRestaurante = new ServiceRestaurante();
            IEnumerable<Restaurante> listaRestaurantes = _ServiceRestaurante.GetRestaurante();
            return new SelectList(listaRestaurantes, "Id", "Nombre", idRestaurante);
        }

        private SelectList listaEstadoMesa(int idEstadoMesa = 0)
        {
            IServiceEstadoMesa _ServiceEstadoMesa = new ServiceEstadoMesa();
            IEnumerable<EstadoMesa> listaEstadoMesa = _ServiceEstadoMesa.GetEstadoMesa();
            return new SelectList(listaEstadoMesa, "Id", "Descripcion", idEstadoMesa);
        }

        private MultiSelectList listaEstadosByID(ICollection<EstadoMesa> estadoById = null)
        {
            IServiceEstadoMesa _ServiceEstadoMesa = new ServiceEstadoMesa();
            IEnumerable<EstadoMesa> listaEstado = _ServiceEstadoMesa.GetEstadoMesa();
            //Selecionar los estados/ Modificar
            int[] listaEstadosSelect = null;
            if (estadoById != null)
            {
                listaEstadosSelect = estadoById.Select(c => c.Id).ToArray();
            }

            return new MultiSelectList(listaEstado, "Id", "Descripcion", listaEstadosSelect);
        }

        //private MultiSelectList listaRestaurantes(ICollection<Restaurante> restaurantes = null)
        //{
        //    IServiceRestaurante _ServiceRestaurante = new ServiceRestaurante();
        //    IEnumerable<Restaurante> listaRestaurantes = _ServiceRestaurante.GetRestaurante();
        //    //Selecionar las categorias / Modificar
        //    int[] listaRestaurantesSelect = null;
        //    if (restaurantes != null)
        //    {
        //        listaRestaurantesSelect = restaurantes.Select(c => c.Id).ToArray();
        //    }

        //    return new MultiSelectList(listaRestaurantes, "IdRestaurante", "Nombre", listaRestaurantesSelect);
        //}


        // GET: Mesa/Create
        public ActionResult Create()
        {
            //Que recursos necesito para crear una mesa

            //restaurante
            ViewBag.IdRestaurante = listaRestaurante();
            ViewBag.IdEstadosById = listaEstadosByID();
            ViewBag.IdEstadoMesa = listaEstadoMesa();
           //estadomesa

            return View();
        }

        // POST: Mesa/Create
        [HttpPost]
        public ActionResult Save(Mesa mesa)
        {
          
            IServiceMesa _ServiceMesa = new ServiceMesa();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
              /*  if (libro.Imagen == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        libro.Imagen = target.ToArray();
                        ModelState.Remove("Imagen");
                    }

                }*/
                if (ModelState.IsValid)
                {
                    Mesa oMesaI = _ServiceMesa.Save(mesa);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    //  Utils.Util.ValidateErrors(this);
                    ViewBag.IdEstadoMesa = listaEstadoMesa(mesa.IdEstadoMesa);
                    ViewBag.IdRestaurante = listaRestaurante(mesa.IdRestaurante);
                   
                    return View("Create", mesa);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Mesa";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Mesa/Edit/5
        public ActionResult Edit(int? id)
        {
            // Que recursos necesito para actualizar un Mesa
            ServiceMesa _ServiceMesa = new ServiceMesa();
            Mesa Mesa = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                //Mesa 
                Mesa = _ServiceMesa.GetMesaByID(Convert.ToInt32(id));
                if (Mesa == null)
                {
                    TempData["Message"] = "No existe el Mesa solicitado";
                    TempData["Redirect"] = "Mesa";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                //Autor
                ViewBag.IdRestaurantes = listaRestaurante();

                //Categorías
                // ViewBag.IdCategoria = listaCategorias(Mesa.Categoria);
                return View(Mesa);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Mesa";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        // POST: Mesa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Mesa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mesa/Delete/5
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
