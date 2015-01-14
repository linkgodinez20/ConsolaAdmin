using System;
using System.ComponentModel.DataAnnotations;

namespace Security.Core.Model
{
    public class SistemasMetadata
    {
        [Required(ErrorMessage = "El campo [Nombre] es obligatorio")]
        [StringLength(32)]
        public String Nombre;

        [StringLength(10)]
        public String Siglas;

        [StringLength(128)]
        [Display(Name = "Descripción")]
        public String Descripcion;

        [Required(ErrorMessage = "El campo [Estatus] es obligatorio")]
        public Boolean Estatus;
    }
}
