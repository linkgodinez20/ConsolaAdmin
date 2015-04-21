using Security.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Services.IServices
{
    public interface IDomiciliosServices
    {
        Domicilios AddAddress(Domicilios domicilio);
        bool EditAddress(Int32 id);
        bool DeleteAddress(Int32 id);
        IQueryable<Domicilios> GetListOfAddresses();
    }
}
