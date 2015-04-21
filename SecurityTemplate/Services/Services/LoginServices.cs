using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Services.IServices;
using Security.Core.Model;
using Security.Core.Repository;
using System.Transactions;
using System.Security.Cryptography;
using Security.Core.Settings;
using Security.Services.Helpers;
using Security.Core.ViewModels;

namespace Security.Services.Services
{
    public class LoginServices: ILoginServices
    {
        private readonly IRepo<Cuentas> repo_cuentas;
        private readonly IDefaultSettings settings;
        private readonly LogInServiceHelper usrHelper;
        private readonly IValidationServices validate;
        private readonly IRepo<Sesiones> repo_sesion;
        private readonly IRepo<Perfiles> repo_perfiles;
        private readonly IRepo<Personas> repo_personas;
        private readonly IRepo<Cuentas_x_Personas> repo_cuentas_x_personas;

        public LoginServices(IRepo<Cuentas> repo_cuentas, IDefaultSettings settings, LogInServiceHelper usrHelper, IValidationServices validate,
            IRepo<Sesiones> repo_sesion, IRepo<Perfiles> repo_perfiles, IRepo<Personas> repo_clientes, IRepo<Cuentas_x_Personas> repo_cuentas_x_clientes)
        {
            this.repo_cuentas = repo_cuentas;
            this.settings = settings;
            this.usrHelper = usrHelper;
            this.repo_sesion = repo_sesion;
            this.repo_perfiles = repo_perfiles;
            this.repo_personas = repo_clientes;
        }
        
        public bool log_in(string usuario, string password, ref string msj)
        {
            /*
            * 1: Obtener con el usuario la salt [ Completado ]
            * 2: Cifrar el psw ingresado con la salt y verificar igualdad en BD -- (valida_log_in()) [ Completado ]
            * 3: Verificar área geográfica permitida y validar dispositivo -- temporalmente no disponible.
            * 4: Obtener matriz de parámetros del sistema (verificar equipos) -- temporalmente no disponible.
            * 5: Obtener perfil -- [ Completado ]
            * 6: Crear sesión -- [ Completado ]
            * 7: Crear objeto con matríz de permisos
            * 8: Verificar si el usuario tiene permisos especiales --
            */

            string salt = usrHelper.getSalt(usuario);


            if (!string.IsNullOrEmpty(salt))
            {
                //generar la cookie en el controller

                string CipherPsw = CipherUtility.Encrypt<RijndaelManaged>(password, settings.HashKey, salt);

                if (validate.Validate_Log_In(usuario, CipherPsw, ref msj))
                {                    
                    int IdCuenta = usrHelper.getCuenta_Id(usuario);
                    
                    // Obtener los valores del perfil.

                    ProfileViewModel perfil = getProfile(IdCuenta); // Regresar a cliente

                    // Configuración los valores iniciales de la sesión del usuario.
                    Sesiones sesion = new Sesiones();
                    
                    sesion.Identificador = Guid.NewGuid();
                    sesion.FechaHoraInicio = DateTime.Now;
                    sesion.OnLine = true;
                    sesion.CierreSesion = false;
                    sesion.Estatus = true;

                    // Creamos la sesión y pasamos el nuevo objeto a una nueva instancia de sesiones para obtener el ID de la misma.
                    Sesiones sesion_result = repo_sesion.Add(sesion);
                    repo_sesion.Save();

                    perfil.Id_Sesion = sesion_result.Id_Sesion;
                    
                }

            }
            else
            {
                msj = "Los datos ingresados no son correctos";
            }
            
            throw new NotImplementedException();
        }

        public bool log_out(int Cuenta_id)
        {
            throw new NotImplementedException();
        }

        public Sesiones addSession(int Cuenta_id)
        {
            throw new NotImplementedException();
        }

        public bool closeSession(int Cuenta_id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cuenta_id"></param>
        /// <returns></returns>
        public ProfileViewModel getProfile(int Cuenta_id)
        {
            ProfileViewModel profile = new ProfileViewModel();

            /*
             * 1: obtener el ID del perfil de la cuenta
             * 2: obtener el ID de la persona, de cuentas_x_personas
             * 3: Llenar profile_vm con los datos necesarios.
             */

            profile.Id_Cuenta = Cuenta_id;

            // 1.- obtener el ID del perfil de la cuenta
            var Qry_idPerfil = from cuenta in repo_cuentas.GetAll()
                               where cuenta.Id_Cuenta == Cuenta_id
                               select cuenta;

            foreach (var perfil in Qry_idPerfil) {
                profile.Id_Perfil = perfil.Id_Perfil;
                profile.Usuario = perfil.Usuario;                
            }

            // 2: obtener el ID de la persona, de cuentas_x_personas
            var Qry_idPersona = from persona in repo_cuentas_x_personas.GetAll()
                                where persona.Id_Cuenta == Cuenta_id
                                select persona.Id_Persona;

            foreach (var persona in Qry_idPersona)
            {
                profile.Id_Persona = persona;
            }

            // 3: Llenar profile_vm con los datos necesarios.
            
            var profileValues = from p in repo_perfiles.GetAll()
                                where p.Id_Sistema == settings.Sistema_ID && p.Id_Perfil == profile.Id_Perfil
                                select p;

            foreach (var profileValue in profileValues) {                
                profile.Nivel = profileValue.Nivel;
                profile.Tipo = profileValue.Tipo;
                profile.Perfil = profileValue.Nombre;
                
            }


            return profile;
        }

    }
}
