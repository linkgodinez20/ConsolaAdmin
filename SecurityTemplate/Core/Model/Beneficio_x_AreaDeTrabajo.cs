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
    
    public partial class Beneficio_x_AreaDeTrabajo
    {
        public int Id { get; set; }
        public byte Id_Beneficio { get; set; }
        public short Id_AreaDeTrabajo { get; set; }
    
        public virtual AreasDeTrabajo AreasDeTrabajo { get; set; }
        public virtual Beneficio Beneficio { get; set; }
    }
}
