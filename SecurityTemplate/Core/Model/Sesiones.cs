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
    
    public partial class Sesiones
    {
        public int Id_Sesion { get; set; }
        public System.Guid Identificador { get; set; }
        public int Id_Cuenta { get; set; }
        public System.DateTime FechaHoraInicio { get; set; }
        public bool OnLine { get; set; }
        public bool CierreSesion { get; set; }
        public Nullable<System.DateTime> UltimoMovimiento { get; set; }
        public Nullable<double> Longitud { get; set; }
        public Nullable<double> Latitud { get; set; }
        public byte Id_Sistema { get; set; }
        public bool Estatus { get; set; }
    
        public virtual Cuentas Cuentas { get; set; }
        public virtual Sistemas Sistemas { get; set; }
    }
}
