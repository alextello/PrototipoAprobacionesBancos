using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.Utilities
{
    public interface ICamposBloqueados
    {
        IEnumerable<string> ListaCampos { get; set; }

        IEnumerable<string> Campos(dynamic clase);
    }
}