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
    
    public partial class Accion_x_Controlador
    {
        public int id { get; set; }
        public int Id_Accion { get; set; }
        public short Id_Controlador { get; set; }
    
        public virtual Acciones Acciones { get; set; }
        public virtual Controladores Controladores { get; set; }
    }
}
