using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrototipoAprobacionesBancos.Models
{
    public partial class Colaborador
    {
        public int IdColaborador { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public int FkIpuesto { get; set; }

        public virtual Puestos FkIpuestoNavigation { get; set; }
    }
}