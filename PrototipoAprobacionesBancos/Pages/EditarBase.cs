using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using ExtensionMethods;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.Pages
{
    public class EditarBase : ComponentBase
    {
        [Parameter]
        public int IdColaborador { get; set; }

        [Inject]
        public ProtoDBContext _context { get; set; }

        [Inject]
        protected NavigationManager _nav { get; set; }

        public Colaborador Colaborador { get; set; }
        public Puestos Puesto { get; set; }

        public List<HistorialAprobacionesEdicion> hst { get; set; }

        protected override Task OnInitializedAsync()
        {
            Puesto = _context.Puestos.FirstOrDefault();
            Colaborador = _context.Colaborador.Include(x => x.FkIpuestoNavigation).
                                    Where(x => x.IdColaborador == IdColaborador).FirstOrDefault();

            return base.OnInitializedAsync();
        }

        protected void HandleValidSubmit()
        {
            _context.Update(Colaborador);
            _context.Update(Puesto);
            _context.SaveChanges();
            StateHasChanged();
            _nav.NavigateTo($"/ListarColaboradores");
        }
    }
}