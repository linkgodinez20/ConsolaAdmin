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
    
    public partial class Parametros_mensaje
    {
        public short Id_ParametroMsg { get; set; }
        public short Id_Parametro { get; set; }
        public string Mensaje { get; set; }
    
        public virtual Parametros Parametros { get; set; }
    }
}
