using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.Pages
{
    public class ListaColaboradoresBase : ComponentBase
    {
        [Inject]
        protected ProtoDBContext _context { get; set; }

        [Inject]
        protected NavigationManager _nav { get; set; }

        public List<Colaborador> Colaboradores { get; set; }

        protected override Task OnInitializedAsync()
        {
            StateHasChanged();
            Colaboradores = _context.Colaborador.Include(x => x.FkIpuestoNavigation).ToList();
            return base.OnInitializedAsync();
        }

        public void IrAPerfilAsync(int id)
        {
            _nav.NavigateTo($"/Editar/{id}");
        }
    }
}