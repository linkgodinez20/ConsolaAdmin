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
    
    public partial class Preguntas
    {
        public Preguntas()
        {
            this.Preguntas_x_Login = new HashSet<Preguntas_x_Login>();
        }
    
        public byte Id_Pregunta { get; set; }
        public string Pregunta { get; set; }
    
        public virtual ICollection<Preguntas_x_Login> Preguntas_x_Login { get; set; }
    }
}