﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Security.Core.Model;
using Security.Core.Repository;
using Security.Core.DefaultSettings;

namespace Security
{
    /// <summary>
    /// Configuración de inicio de la aplicación
    /// </summary>
    public class DefaultSettings: IDefaultSettings
    {
        // propiedades

        private string sistema_Nombre;
        private byte sistema_iD = 1;
        private string sistema_HashKey;
        private string sistema_ConnectionString;

        private string directorio_PersonasFoto;
        private string directorio_PaisesBanera;
        private string directorio_Imagenes;
        private string directorio_Iconos;

        private string parametros_caducidadEquipoPrincipal;
        private string parametros_caducidadEquipoSecundario;
        private string parametros_caducidadEquipoTemporal;

        // Inyección de componentes

        private readonly IRepo<Sistemas> repo_sistemas;
        private readonly IRepo<Directorio_tipo> repo_directorioTipo;
        private readonly IRepo<Directorios> repo_directorios;
        private readonly IRepo<Parametros_configuracion> repo_parametrosConfig;

        public DefaultSettings(IRepo<Sistemas> repo_sistemas, IRepo<Directorio_tipo> repo_directorioTipo, IRepo<Directorios> repo_directorios)
        {
            this.repo_sistemas = repo_sistemas;
            this.repo_directorioTipo = repo_directorioTipo;
            this.repo_directorios = repo_directorios;
        }

        public string Sistema_Nombre
        {
            get
            {
                var nombre = from n in repo_sistemas.GetAll()
                             where n.Id_Sistema == this.sistema_iD
                             select n.Nombre;

                this.sistema_Nombre = Convert.ToString(nombre);

                return sistema_Nombre;
            }

        }

        public byte Sistema_iD
        {
            get
            {
                return sistema_iD;
            }
        }

        public string Sistema_HashKey
        {
            get
            {
                this.sistema_HashKey = "LOZL4kVrTLqBU8bBjFD/beWvA0IY2sju";
                return this.sistema_HashKey;
            }
        }

        public string Sistema_ConnectionString
        {
            get
            {
                this.sistema_ConnectionString = "SecurityEntities";
                return this.sistema_ConnectionString;
            }
        }

        public string Directorio_PersonasFoto
        {
            get
            {
                //var dirTipo = "Media";

                //var dirMedia = from d in repo_directorioTipo.GetAll()                               
                //               where d.Id_Sistema == this.sistema_iD
                //               select d.Nombre;

                return this.directorio_PersonasFoto = "~/Media/Personas/Foto";

            }

        }

        public string Directorio_PaisesBanera
        {
            get
            {
                //var dirTipo = "Media";

                //var dirMedia = from d in repo_directorioTipo.GetAll()                               
                //               where d.Id_Sistema == this.sistema_iD
                //               select d.Nombre;

                return this.directorio_PaisesBanera = "~/Media/Paises/Banderas";

            }

        }

        public string Directorio_Imagenes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Directorio_Iconos
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}