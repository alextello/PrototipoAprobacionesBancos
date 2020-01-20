using Microsoft.AspNetCore.Components;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using ExtensionMethods;

namespace PrototipoAprobacionesBancos.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        protected ProtoDBContext _context { get; set; }

        [Inject]
        protected NavigationManager _nav { get; set; }

        public Colaborador Colaborador { get; set; } = new Colaborador();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected void HandleValidSubmit()
        {
            _context.Colaborador.Add(Colaborador);
            _context.SaveChanges();
            _nav.NavigateTo("/");
        }
    }
}