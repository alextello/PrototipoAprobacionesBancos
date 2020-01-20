using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace PrototipoAprobacionesBancos.Models
{
    public partial class ProtoDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProtoDBContext()
        {
        }

        public ProtoDBContext(DbContextOptions<ProtoDBContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Accion { get; set; }
        public string Tabla { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string ValorOriginal = String.Empty;
        public string ValorActual = String.Empty;
        public virtual DbSet<CamposQueNecesitanAprobacion> CamposQueNecesitanAprobacion { get; set; }
        public virtual DbSet<Colaborador> Colaborador { get; set; }
        public virtual DbSet<HistorialAprobacionesEdicion> HistorialAprobacionesEdicion { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ProtoString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CamposQueNecesitanAprobacion>(entity =>
            {
                entity.HasKey(e => e.IdCamposQueNecesitanAprobacion)
                    .HasName("aprobacionparaedicion_pk");

                entity.Property(e => e.Campo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tabla)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colaborador>(entity =>
            {
                entity.HasKey(e => e.IdColaborador);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkIpuesto).HasColumnName("FK_IPuesto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIpuestoNavigation)
                    .WithMany(p => p.Colaborador)
                    .HasForeignKey(d => d.FkIpuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Colaborad__FK_IP__4F7CD00D");
            });

            modelBuilder.Entity<HistorialAprobacionesEdicion>(entity =>
            {
                entity.HasKey(e => e.IdHistorialAprobacionesEdicion)
                    .HasName("historialaprobacionesedicion_pk");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");

                entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");

                entity.Property(e => e.FkIdCamposQueNecesitanAprobacion).HasColumnName("FK_IdCamposQueNecesitanAprobacion");

                entity.Property(e => e.Idregistro).HasColumnName("IDRegistro");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioAprueba)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioSolicita)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValorAnterior)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ValorNuevo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdCamposQueNecesitanAprobacionNavigation)
                    .WithMany(p => p.HistorialAprobacionesEdicion)
                    .HasForeignKey(d => d.FkIdCamposQueNecesitanAprobacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("historialaprobacionesedicion_camposquenecesitanaprobacion_fk");
            });

            modelBuilder.Entity<Puestos>(entity =>
            {
                entity.HasKey(e => e.IdPuesto)
                    .HasName("PK__Puestos__ADAC6B9C0CC3CCB7");

                entity.Property(e => e.IdPuesto).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<HistorialAprobacionesEdicion>().HasQueryFilter(x => x.Estado == "1");
            modelBuilder.Entity<CamposQueNecesitanAprobacion>().HasQueryFilter(x => (bool)x.Activo);
            OnModelCreatingPartial(modelBuilder);
        }

        public override int SaveChanges()
        {
            RegistrarModificaciones();
            return base.SaveChanges();
        }

        private void RegistrarModificaciones()
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole("WWW_GTH_EXPEDIENTES_SECRETARIAS"))
            {
                var modifiedEntities = base.ChangeTracker.Entries()
                                      .Where(p => p.State == EntityState.Modified).ToList();
                foreach (var change in modifiedEntities)
                {
                    Tabla = change.Entity.GetType().Name;

                    foreach (var prop in change.OriginalValues.Properties)
                    {
                        if (prop.IsPrimaryKey())
                        {
                            Id = Convert.ToInt32(change.OriginalValues[prop].ToString());
                        }

                        ValorOriginal = change.OriginalValues[prop] != null ? change.OriginalValues[prop].ToString() : "";
                        ValorActual = change.CurrentValues[prop] != null ? change.CurrentValues[prop].ToString() : "";

                        if (!ValorOriginal.Equals("") && !ValorOriginal.Equals(ValorActual))
                        {
                            var record = this.CamposQueNecesitanAprobacion.Where(x => x.Tabla == Tabla && x.Campo == prop.Name).FirstOrDefault();

                            if (record != null)
                            {
                                base.Add(new HistorialAprobacionesEdicion
                                {
                                    Estado = "1",
                                    FechaSolicitud = DateTime.UtcNow,
                                    ValorAnterior = ValorOriginal,
                                    ValorNuevo = ValorActual,
                                    Idregistro = Id,
                                    FkIdCamposQueNecesitanAprobacion = record.IdCamposQueNecesitanAprobacion
                                });
                                //change.State = EntityState.Unchanged;
                                change.Property(prop.Name).IsModified = false;
                                change.State = EntityState.Detached;
                                //change.Reload();
                            }
                        }
                    }
                }
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}