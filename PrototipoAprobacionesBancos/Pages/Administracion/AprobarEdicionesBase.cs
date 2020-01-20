using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ExtensionMethods;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.Pages.Administracion
{
    public class AprobarEdicionesBase : ComponentBase
    {
        [Inject]
        public ProtoDBContext _context { get; set; }

        public List<HistorialAprobacionesEdicion> Solicitudes { get; set; }

        protected override Task OnInitializedAsync()
        {
            Solicitudes = _context.HistorialAprobacionesEdicion
                                  .Include(x => x.FkIdCamposQueNecesitanAprobacionNavigation)
                                  .ToList();

            return base.OnInitializedAsync();
        }

        protected void Aprobar(HistorialAprobacionesEdicion id)
        {
            _context.Database.ExecuteSqlRaw($"UPDATE {id.FkIdCamposQueNecesitanAprobacionNavigation.Tabla} " +
                $"set {id.FkIdCamposQueNecesitanAprobacionNavigation.Campo} = '{id.ValorNuevo}'");
            id.Estado = "2";
            id.FechaAprobacion = DateTime.UtcNow;
            _context.HistorialAprobacionesEdicion.Update(id);
            _context.SaveChanges();
            Solicitudes.Remove(id);
            StateHasChanged();
        }

        protected void Denegar(HistorialAprobacionesEdicion id)
        {
        }
    }
}