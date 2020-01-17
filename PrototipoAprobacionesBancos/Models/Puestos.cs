using System;
using System.Collections.Generic;

namespace PrototipoAprobacionesBancos.Models
{
    public partial class Puestos
    {
        public Puestos()
        {
            Colaborador = new HashSet<Colaborador>();
        }

        public int IdPuesto { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Colaborador> Colaborador { get; set; }
    }
}