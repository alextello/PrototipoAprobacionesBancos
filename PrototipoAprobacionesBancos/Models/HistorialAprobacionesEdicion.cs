using System;
using System.Collections.Generic;

namespace PrototipoAprobacionesBancos.Models
{
    public partial class HistorialAprobacionesEdicion
    {
        public int IdHistorialAprobacionesEdicion { get; set; }
        public int? Idregistro { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string UsuarioAprueba { get; set; }
        public string UsuarioSolicita { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public int FkIdCamposQueNecesitanAprobacion { get; set; }

        public virtual CamposQueNecesitanAprobacion FkIdCamposQueNecesitanAprobacionNavigation { get; set; }
    }
}