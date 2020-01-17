using Microsoft.EntityFrameworkCore.Internal;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrototipoAprobacionesBancos.ExtensionMethods
{
    public static partial class CustomExtensions
    {
        public static IQueryable Query(this ProtoDBContext context, string entityName) =>
            context.Query(context.Model.FindEntityType(entityName).ClrType);

        public static IQueryable Query(this ProtoDBContext context, Type entityType) =>
            (IQueryable)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType);
    }
}