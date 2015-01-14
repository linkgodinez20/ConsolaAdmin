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
    
    public partial class Personas
    {
        public Personas()
        {
            this.LogIn = new HashSet<LogIn>();
            this.Contacto = new HashSet<Contacto>();
            this.Cuentas = new HashSet<Cuentas>();
            this.Domicilios = new HashSet<Domicilios>();
        }
    
        public int Id_Persona { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string Email { get; set; }
        public byte Id_Genero { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public string Homoclave { get; set; }
        public string CURP { get; set; }
        public string Foto { get; set; }
        public bool Estatus { get; set; }
    
        public virtual Genero Genero { get; set; }
        public virtual ICollection<LogIn> LogIn { get; set; }
        public virtual ICollection<Contacto> Contacto { get; set; }
        public virtual ICollection<Cuentas> Cuentas { get; set; }
        public virtual ICollection<Domicilios> Domicilios { get; set; }
    }
}