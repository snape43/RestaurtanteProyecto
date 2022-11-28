using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Util;
using Web.Utils;
using Web.ViewModel;

namespace Web.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }

            //lista de clientes
            //Lista de productos
            ViewBag.idCliente = listaClientes();
            ViewBag.idMesas = listaMesas();
            //ViewBag.idProductos = listaProductos();
            ViewBag.PedidoDetalle = Carrito.Instancia.Items;
            //IEnumerable<Pedido> lista = null;
            //try
            //{
            //    IServicePedido _SevicePedido = new ServicePedido();
            //    lista = _SevicePedido.GetPedido();
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, MethodBase.GetCurrentMethod());
            //    TempData["Message"] = "Error al procesar los datos!" + ex.Message;
            //    return RedirectToAction("Default", "Error");
            //}
            return View();
        }
        private SelectList listaClientes()
        {
            //Lista de Clientes
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> listaClientes = _ServiceUsuario.GetCliente();

            return new SelectList(listaClientes, "Id", "IdTipoUsuario");
        }
            
        private SelectList listaMesas()
        {
            //Lista de Mesas
            IServiceMesa _ServiceMesa = new ServiceMesa();
            IEnumerable<Mesa> listaMesas = _ServiceMesa.GetMesa();

            return new SelectList(listaMesas, "Id", "NumeroMesa");

        }
        private SelectList listaProductos()
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IEnumerable<Producto> listaProducto = _ServiceProducto.GetProducto();

            return new SelectList(listaProducto, "Id", "IdProducto");

        }
        private ActionResult DetalleCarrito()
        {
            return PartialView("_DetallePedido", Carrito.Instancia.Items);
        }

        public ActionResult actualizarCantidad(int idProducto, int cantidad)
        {
            ViewBag.DetallePedido = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idProducto, cantidad);
            TempData.Keep();
            return PartialView("_DetallePedido", Carrito.Instancia.Items);
        }

        public ActionResult ordenarProducto(int? idProducto)
        {
            int cantidadProductos = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idProducto);
            return PartialView("_PedidoCantidad");
        }
        public ActionResult actualizarPedidoCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("_PedidoCantidad");
        }

        public ActionResult eliminiarProducto(int? idProducto)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem((int)idProducto);
            return PartialView("_DetallePedido", Carrito.Instancia.Items);
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable<Pedido> lista = null;
            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedido();
                return View(lista);
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Pedido";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {
            ServicePedido _ServicePedido = new ServicePedido();
            Pedido pedido = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                pedido = _ServicePedido.GetPedidoByID(Convert.ToInt32(id));
                if (pedido == null)
                {
                    TempData["Message"] = "No existe el Pedido solicitado";
                    TempData["Redirect"] = "Pedido";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(pedido);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Pedido";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Mesero)]
        public ActionResult Save(Pedido pedido)
        {
            try
            {
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Pedido", "Seleccione los productos a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {
                    //Obtener datos usuario logueado
                    Usuario oUsuario = (Usuario)Session["User"];
                    //Asignar idUsuario que se encuentra logueado
                    //pedido.Id = 1;
                    pedido.IdCliente = 3;
                    pedido.IdEmpleado = oUsuario.Id;
                    pedido.IdMesa = pedido.IdMesa;
                    pedido.IdEstadoPedido = 4;
                    pedido.Observaciones = pedido.Observaciones;
                    pedido.FechaPedido = pedido.FechaPedido;

                    var listaDetalle = Carrito.Instancia.Items;
                    //Agregar cada línea de detalle a la pedido
                    foreach (var item in listaDetalle)
                    {
                        DetallePedido detallePedido = new DetallePedido();
                        detallePedido.IdPedido = item.IdPedido;
                        detallePedido.IdProducto = item.IdProducto;
                        detallePedido.Cantidad = item.Cantidad;
                        detallePedido.Subtotal = item.Subtotal;
                        detallePedido.Nota = item.Nota;

                        pedido.DetallePedido.Add(detallePedido);
                       

                    }
                }
                //guardar pedido
                IServicePedido _ServicePedido = new ServicePedido();
                Pedido pedidoSave = _ServicePedido.Save(pedido);

                // Limpia el Carrito de compras
                Carrito.Instancia.eliminarCarrito();
                //TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Pedido", "Pedido guardada satisfactoriamente!", SweetAlertMessageType.success);
                // Reporte pedido
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Pedido";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult salvarFactura(Pedido pedido)
        {
            //detalle pedido subtotal
            //detalle pedido id pedido
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return View();

        }

        //public ActionResult graficoPedido()
        //{
        //    //Documentación chartjs https://www.chartjs.org/docs/latest/
        //    IServicePedido _ServicePedido = new ServicePedido();
        //    ViewModelGrafico grafico = new ViewModelGrafico();
        //    _ServicePedido.GetPedidoCountDate(out string etiquetas, out string valores);


        //    //Tipos: bar , bubble , doughnut , pie , line , polarArea 
        //    grafico.tipo = "bar";
        //    ViewBag.grafico = grafico;
        //    return View();
        //}
    }
}
