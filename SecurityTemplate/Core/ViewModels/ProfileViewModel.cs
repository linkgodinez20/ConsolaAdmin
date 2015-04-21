using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Core.ViewModels
{
    public class ProfileViewModel
    {
        public int Id_Cuenta { get; set; }
        public int Id_Persona { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Email { get; set; }
        public Int16 Id_Perfil { get; set; }
        public string Perfil { get; set; }
        public string Tipo { get; set; }
        public string Nivel { get; set; }
        public int Id_Sesion { get; set; }
        public string Foto { get; set; }
    }
}
