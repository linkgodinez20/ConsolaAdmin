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
    
    public partial class Equipos
    {
        public short Id_Equipo { get; set; }
        public string Hostname { get; set; }
        public string IP { get; set; }
        public string MacAddress { get; set; }
        public byte Id_EquipoTipo { get; set; }
        public string Descripcion { get; set; }
        public System.Data.Entity.Spatial.DbGeography Ubiacion { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaCaducidad { get; set; }
        public int Id_Cuenta { get; set; }
        public byte Id_Sistema { get; set; }
        public bool Estatus { get; set; }
    
        public virtual Cuentas Cuentas { get; set; }
        public virtual Equipos_tipo Equipos_tipo { get; set; }
        public virtual Sistemas Sistemas { get; set; }
    }
}
