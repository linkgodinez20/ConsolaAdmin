//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Security.Core.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Baja_motivos
    {
        public Baja_motivos()
        {
            this.Cuentas = new HashSet<Cuentas>();
        }
    
        public byte Id_MotivoBaja { get; set; }
        public string Descripcion { get; set; }
        public byte Id_Baja { get; set; }
    
        public virtual Baja Baja { get; set; }
        public virtual ICollection<Cuentas> Cuentas { get; set; }
    }
}
