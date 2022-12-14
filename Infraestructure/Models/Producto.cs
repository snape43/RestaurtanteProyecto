//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductoMetadata))]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.DetallePedido = new HashSet<DetallePedido>();
            this.Restaurante = new HashSet<Restaurante>();
        }
    
        public int Id { get; set; }
        public byte[] Foto { get; set; }
        public string Descripcion { get; set; }
        public string Ingredientes { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public int IdCategoriaProducto { get; set; }
    
        public virtual CategoriaProducto CategoriaProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Restaurante> Restaurante { get; set; }
    }
}
