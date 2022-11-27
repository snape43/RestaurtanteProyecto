using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;


namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelDetallePedido> Items { get; private set; }

        public static readonly Carrito Instancia;

        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelDetallePedido>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }
        protected Carrito() { }

        public String AgregarItem(int productoId)
        {
            ViewModelDetallePedido nuevoItem = new ViewModelDetallePedido(productoId);
            String mensaje = "";
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.IdProducto == productoId))
                {
                    ViewModelDetallePedido item = Items.Find(x => x.IdProducto == productoId);
                    item.Cantidad++;
                }
                else
                {
                    nuevoItem.Cantidad = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Pedido producto", "Producto agregado al pedido", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Pedido producto", "Producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }
        public String SetItemCantidad(int productoId, int Cantidad)
        {
            String mensaje = "";
            if (Cantidad == 0)
            {
                EliminarItem(productoId);
                mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Producto eliminado", SweetAlertMessageType.success);

            }
            else
            {
                ViewModelDetallePedido actualizarItem = new ViewModelDetallePedido(productoId);

                if (Items.Exists(x => x.IdProducto == productoId))
                {
                    ViewModelDetallePedido item = Items.Find(x => x.IdProducto == productoId);
                    item.Cantidad = Cantidad;
                    mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Cantidad actualizada", SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }
        public String EliminarItem(int productoId)
        {
            String mensaje = "El producto no existe";
            if (Items.Exists(x => x.IdProducto == productoId))
            {
                var itemEliminar = Items.Single(x => x.IdProducto == productoId);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Producto Eliminado", SweetAlertMessageType.success);

            }
            return mensaje;

        }
        public decimal GetTotal()
        {
            decimal total = 0;
            total = Items.Sum(x => x.Subtotal);

            return total;
        }
        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.Cantidad);

            return total;
        }
        public void eliminarCarrito()
        {
            Items.Clear();
        }

    }
}