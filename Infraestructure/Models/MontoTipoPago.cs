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

    [MetadataType(typeof(MontoTipoPagoMetadata))]
    public partial class MontoTipoPago
    {
        public int Id { get; set; }
        public int IdFacturaEncabezado { get; set; }
        public int IdTipoPago { get; set; }
        public Nullable<decimal> Monto { get; set; }
    
        public virtual FacturaEncabezado FacturaEncabezado { get; set; }
        public virtual TipoPago TipoPago { get; set; }
    }
}
