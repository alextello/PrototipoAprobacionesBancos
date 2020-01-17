using Microsoft.EntityFrameworkCore;
using PrototipoAprobacionesBancos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;

namespace PrototipoAprobacionesBancos.ExtensionMethods
{
    public static class LockedProperties
    {
        public static List<string> PropiedadesBloqueadas(this Colaborador query)
        {
            List<string> lst = new List<string>();
            var props = query.GetType().GetProperties().ToList();
            foreach (var prop in props)
            {
                lst.Add(prop.Name);
            }
            return lst;
        }

        public static string Tabla<T>(this T col) where T : Type
        {
            ProtoDBContext db = new ProtoDBContext();
            var entityType = db.Model.FindEntityType((col));
            return entityType.GetTableName();
        }
    }
}