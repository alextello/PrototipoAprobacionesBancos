using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PrototipoAprobacionesBancos.ExtensionMethods;
using PrototipoAprobacionesBancos.Models;
using PrototipoAprobacionesBancos.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public List<HistorialAprobacionesEdicion> hst { get; set; }

        protected override Task OnInitializedAsync()
        {
            Colaborador = _context.Colaborador.Include(x => x.FkIpuestoNavigation).
                                FirstOrDefault(x => x.IdColaborador == IdColaborador);

            return base.OnInitializedAsync();
        }

        protected void HandleValidSubmit()
        {
            _context.Update(Colaborador);
            _context.SaveChanges();
            StateHasChanged();
            _nav.NavigateTo($"/ListarColaboradores");
        }
    }
}