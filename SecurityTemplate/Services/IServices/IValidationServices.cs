using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Services.IServices
{
    public interface IValidationServices
    {
        bool ValidateAccess(string Controller, string Action, ref string msj);
        bool Validate_Log_In(string usuario, string password, ref string msj);

        bool Validate_Device();
    }
}
