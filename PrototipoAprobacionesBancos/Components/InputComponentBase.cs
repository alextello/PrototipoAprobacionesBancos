using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PrototipoAprobacionesBancos.ExtensionMethods;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.Components
{
    public class InputComponentBase : ComponentBase
    {
        public bool Flag { get; set; }

        //[Parameter]
        //public Expression<Func<T>> For { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string type { get; set; }

        [Parameter]
        public string Campo { get; set; }

        [Parameter]
        public Type Tipo { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public string Tabla { get; set; }

        private string _value;

        [Parameter]
        public string BindingValue
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                BindingValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<string> BindingValueChanged { get; set; }

        [Inject]
        public ProtoDBContext _context { get; set; }

        protected override Task OnInitializedAsync()
        {
            var tabla = Tipo.Tabla();
            Flag = _context.HistorialAprobacionesEdicion.Include(x => x.FkIdCamposQueNecesitanAprobacionNavigation)
                .Where(x => x.FkIdCamposQueNecesitanAprobacionNavigation.Tabla == tabla &&
                        x.FkIdCamposQueNecesitanAprobacionNavigation.Campo == Campo &&
                        x.Idregistro == Id)
                .Any();
            return base.OnInitializedAsync();
        }
    }
}