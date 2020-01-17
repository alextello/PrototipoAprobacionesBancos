using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.Utilities
{
    public class CamposBloqueados
    {
        private readonly ProtoDBContext _context;

        public CamposBloqueados(ProtoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<string> ListaCampos { get; set; }
    }
}