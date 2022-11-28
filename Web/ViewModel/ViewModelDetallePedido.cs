using Infraestructure.Models;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class ViewModelDetallePedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Nota { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Precio
        {
            get { return Producto.Precio; }
        }
        public virtual Producto Producto { get; set; }

        public decimal Subtotal 
        {
            get { return calculoSubtotal(); }
        }
        private decimal calculoSubtotal()
        {
            return this.Precio * this.Cantidad;
        }

        public ViewModelDetallePedido(int IdProducto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            this.IdProducto = IdProducto;
            this.Producto = _ServiceProducto.GetProductoByID(IdProducto);
        }
            

    }
}