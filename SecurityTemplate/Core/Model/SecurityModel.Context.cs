﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SecurityEntities : DbContext
    {
        public SecurityEntities()
            : base("name=SecurityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actividades> Actividades { get; set; }
        public virtual DbSet<AreasDeTrabajo> AreasDeTrabajo { get; set; }
        public virtual DbSet<Baja> Baja { get; set; }
        public virtual DbSet<Baja_motivos> Baja_motivos { get; set; }
        public virtual DbSet<Beneficio> Beneficio { get; set; }
        public virtual DbSet<Beneficio_tipo> Beneficio_tipo { get; set; }
        public virtual DbSet<Beneficio_x_Perfiles> Beneficio_x_Perfiles { get; set; }
        public virtual DbSet<Benficio_x_Cuenta> Benficio_x_Cuenta { get; set; }
        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Contacto_medio> Contacto_medio { get; set; }
        public virtual DbSet<Contacto_tipo> Contacto_tipo { get; set; }
        public virtual DbSet<Cuentas> Cuentas { get; set; }
        public virtual DbSet<Directorio_tipo> Directorio_tipo { get; set; }
        public virtual DbSet<Directorios> Directorios { get; set; }
        public virtual DbSet<Domicilios> Domicilios { get; set; }
        public virtual DbSet<Entidades> Entidades { get; set; }
        public virtual DbSet<Equipos> Equipos { get; set; }
        public virtual DbSet<Equipos_tipo> Equipos_tipo { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<LogIn> LogIn { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Menu_categoria> Menu_categoria { get; set; }
        public virtual DbSet<Municipios> Municipios { get; set; }
        public virtual DbSet<Paginas> Paginas { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Parametros_configuracion> Parametros_configuracion { get; set; }
        public virtual DbSet<Parametros_grupo> Parametros_grupo { get; set; }
        public virtual DbSet<Parametros_mensaje> Parametros_mensaje { get; set; }
        public virtual DbSet<Parametros_tipoDato> Parametros_tipoDato { get; set; }
        public virtual DbSet<Perfiles> Perfiles { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Permisos_AreaDeTrabajo_x_Cuenta> Permisos_AreaDeTrabajo_x_Cuenta { get; set; }
        public virtual DbSet<Permisos_Cuentas_x_Actividades> Permisos_Cuentas_x_Actividades { get; set; }
        public virtual DbSet<Permisos_Perfiles_x_Actividades> Permisos_Perfiles_x_Actividades { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Preguntas> Preguntas { get; set; }
        public virtual DbSet<Preguntas_x_Login> Preguntas_x_Login { get; set; }
        public virtual DbSet<Sesiones> Sesiones { get; set; }
        public virtual DbSet<Sistemas> Sistemas { get; set; }
    }
}
