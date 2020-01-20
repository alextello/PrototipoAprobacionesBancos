using Microsoft.EntityFrameworkCore;
using System;

namespace ExtensionMethods
{
    public static class TablaDeEntidad
    {
        public static string Tabla<T>(this T col, DbContext db) where T : Type
        {
            //ProtoDBContext db = new ProtoDBContext();
            Microsoft.EntityFrameworkCore.Metadata.IEntityType entityType = db.Model.FindEntityType((col));
            return entityType.GetTableName();
        }
    }
}