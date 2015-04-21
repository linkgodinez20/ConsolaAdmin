using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Core.Settings
{
    public interface IDefaultSettings
    {
        string HashKey { get; }
        byte Sistema_ID { get; }

    }
}
