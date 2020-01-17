using System;
using System.Collections.Generic;

namespace PrototipoAprobacionesBancos.Models
{
    public partial class CamposQueNecesitanAprobacion
    {
        public CamposQueNecesitanAprobacion()
        {
            HistorialAprobacionesEdicion = new HashSet<HistorialAprobacionesEdicion>();
        }

        public int IdCamposQueNecesitanAprobacion { get; set; }
        public string Tabla { get; set; }
        public string Campo { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<HistorialAprobacionesEdicion> HistorialAprobacionesEdicion { get; set; }
    }
}