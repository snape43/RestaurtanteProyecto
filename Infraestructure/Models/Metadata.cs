using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class PedidoMetadata
    {

        [Display(Name = "Número de Pedido")]
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Empleado")]
        public int IdEmpleado { get; set; }

        [Display(Name = "Mesa Asignada")]
        public int IdMesa { get; set; }

        [Display(Name = "Observaciones adicionales al Pedido")]
        public string Observaciones { get; set; }

        [Display(Name = "Estado de Pedido")]
        public int IdEstadoPedido { get; set; }

        [Display(Name = "Fecha")]
        public Nullable<System.DateTime> FechaPedido { get; set; }

        [Display(Name = "Productos elegidos")]
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }

        [ForeignKey("IdEstadoPedido")]
        public virtual EstadoPedido EstadoPedido { get; set; }

        public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; }

        [ForeignKey("IdMesa")]
        public virtual Mesa Mesa { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("IdClienteEmpleado")]
        public virtual Usuario Usuario1 { get; set; }

    }

    internal partial class ProductoMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Foto del producto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public byte[] Foto { get; set; }

        [Display(Name = "Nombre de Producto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]

        public string Descripcion { get; set; }

        [Display(Name = "Ingredientes")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [StringLength(1000)]

        public string Ingredientes { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Precio de Producto")]
        [Range(0, 10000.99)]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]

        public Nullable<decimal> Precio { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        public int IdCategoriaProducto { get; set; }

        [ForeignKey("IdCategoriaProducto")]
        [Display(Name = "Categoría del producto")]

        public virtual CategoriaProducto CategoriaProducto { get; set; }
  
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }

        [Display(Name = "Categoría del producto")]
        public virtual ICollection<Restaurante> Restaurante { get; set; }
    }

    internal partial class DetallePedidoMetadata
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        [Display(Name = "Cantidad de Producto")]
        public Nullable<int> Cantidad { get; set; }

        public Nullable<decimal> Subtotal { get; set; }

        [Display(Name = "Notas del Producto")]
        public string Nota { get; set; }

        [ForeignKey("IdPedido")]
        public virtual Pedido Pedido { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
    }

    internal partial class MesaMetadata
    {
        public int Id { get; set; }

        public int IdRestaurante { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Numero de Mesa")]
         public string NumeroMesa { get; set; }


        [Display(Name = "Capacidad")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]
        public Nullable<int> Capacidad { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<bool> Estado { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdEstadoMesa { get; set; }

        [ForeignKey("IdEstadoMesa")]
        public virtual EstadoMesa EstadoMesa { get; set; }
       
        public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; }

        [ForeignKey("IdRestaurante")]
        [Display(Name = "Restaurante")]

        public virtual Restaurante Restaurante { get; set; }

        [Display(Name = "Pedido")]
        public virtual ICollection<Pedido> Pedido { get; set; }
    }

    internal partial class RestauranteMetadata
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Mesa> Mesa { get; set; }
       
        public virtual ICollection<Producto> Producto { get; set; }
    }

    internal partial class UsuarioMetadata
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public int IdTipoUsuario { get; set; }

       
        public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; }
      
        public virtual ICollection<FacturaEncabezado> FacturaEncabezado1 { get; set; }
        
        public virtual ICollection<Pedido> Pedido { get; set; }
       
        public virtual ICollection<Pedido> Pedido1 { get; set; }

        [ForeignKey("IdTipoUsuario")]
        public virtual TipoUsuario TipoUsuario { get; set; }
    }

    internal partial class TipoUsuarioMetadata
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }

    internal partial class MontoTipoPagoMetadata
    {
        public int Id { get; set; }
        public int IdFacturaEncabezado { get; set; }
        public int IdTipoPago { get; set; }
        public Nullable<decimal> Monto { get; set; }

        [ForeignKey("IdFacturaEncabezado")]
        public virtual FacturaEncabezado FacturaEncabezado { get; set; }
        [ForeignKey("IdTipoPago")]
        public virtual TipoPago TipoPago { get; set; }
    }

    internal partial class FacturaEncabezadoMetadata
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdMesa { get; set; }
        public Nullable<System.DateTime> FechaFactura { get; set; }
        public int IdPedido { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> Impuesto { get; set; }
        public Nullable<decimal> Total { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("IdEmpleado")]
        public virtual Usuario Usuario1 { get; set; }
        [ForeignKey("IdMesa")]
        public virtual Mesa Mesa { get; set; }
        [ForeignKey("IdPedido")]
        public virtual Pedido Pedido { get; set; }
        public virtual ICollection<MontoTipoPago> MontoTipoPago { get; set; }
    }

    internal partial class TipoPagoMetadata
    {
        public int Id { get; set; }
        public string TipoPago1 { get; set; }
        public virtual ICollection<MontoTipoPago> MontoTipoPago { get; set; }
    }

    internal partial class EstadoPedidoMetadata
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }

    internal partial class EstadoMesaMetadata
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Mesa> Mesa { get; set; }
    }


    internal partial class CategoriaProductoMetadata
    {
        public int Id { get; set; }
        public string NombreCategoria { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }

}
