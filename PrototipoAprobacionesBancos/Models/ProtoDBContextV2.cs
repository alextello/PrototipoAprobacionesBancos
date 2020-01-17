using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrototipoAprobacionesBancos.Models
{
    public partial class ProtoDBContextV2 : ProtoDBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistorialAprobacionesEdicion>().HasQueryFilter(x => x.Estado == "1");
            base.OnModelCreating(modelBuilder);
        }
    }
}