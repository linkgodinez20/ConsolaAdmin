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
    
    public partial class AreasDeTrabajo_x_Perfiles
    {
        public int id { get; set; }
        public short Id_AreaDeTrabajo { get; set; }
        public short Id_Perfil { get; set; }
    
        public virtual AreasDeTrabajo AreasDeTrabajo { get; set; }
        public virtual Perfiles Perfiles { get; set; }
    }
}
