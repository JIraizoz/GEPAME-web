using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GEPAME.Models
{
    public partial class GEPAMEContext : DbContext
    {
        public GEPAMEContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AccesoVehiculo> AccesoVehiculo { get; set; }
        public virtual DbSet<AsignacionIncidencia> AsignacionIncidencia { get; set; }
        public virtual DbSet<Incidencia> Incidencia { get; set; }
        public virtual DbSet<Posicion> Posicion { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TipoIncidencia> TipoIncidencia { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccesoVehiculo>(entity =>
            {
                entity.HasKey(e => new { e.IdVehiculo, e.IdUsuario });

                entity.ToTable("ACCESO_VEHICULO");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.AccesoVehiculo)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ACCESO_VEHICULO_USUARIO");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.AccesoVehiculo)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_ACCESO_VEHICULO_VEHICULO");
            });

            modelBuilder.Entity<AsignacionIncidencia>(entity =>
            {
                entity.HasKey(e => new { e.IdVehiculo, e.TipoIncidencia, e.IdIncidencia });

                entity.ToTable("ASIGNACION_INCIDENCIA");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoIncidencia)
                    .HasColumnName("tipoIncidencia")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdIncidencia)
                    .HasColumnName("idIncidencia")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.AsignacionIncidencia)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_ASIGNACION_INCIDENCIA_VEHICULO");

                entity.HasOne(d => d.Incidencia)
                    .WithMany(p => p.AsignacionIncidencia)
                    .HasForeignKey(d => new { d.TipoIncidencia, d.IdIncidencia })
                    .HasConstraintName("FK_ASIGNACION_INCIDENCIA_INCIDENCIA");
            });

            modelBuilder.Entity<Incidencia>(entity =>
            {
                entity.HasKey(e => new { e.TipoIncidencia, e.IdIncidencia });

                entity.ToTable("INCIDENCIA");

                entity.Property(e => e.TipoIncidencia)
                    .HasColumnName("tipoIncidencia")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdIncidencia)
                    .HasColumnName("idIncidencia")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Utm)
                    .IsRequired()
                    .HasColumnName("utm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoIncidenciaNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.TipoIncidencia)
                    .HasConstraintName("FK_INCIDENCIA_TIPO_INCIDENCIA");
            });

            modelBuilder.Entity<Posicion>(entity =>
            {
                entity.HasKey(e => new { e.IdVehiculo, e.Fecha });

                entity.ToTable("POSICION");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.IdIncidencia)
                    .IsRequired()
                    .HasColumnName("idIncidencia")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TipoIncidencia)
                    .IsRequired()
                    .HasColumnName("tipoIncidencia")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Utm)
                    .IsRequired()
                    .HasColumnName("utm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Posicion)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_POSICION_VEHICULO");

                entity.HasOne(d => d.Incidencia)
                    .WithMany(p => p.Posicion)
                    .HasForeignKey(d => new { d.TipoIncidencia, d.IdIncidencia })
                    .HasConstraintName("FK_POSICION_INCIDENCIA");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("ROL");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoIncidencia>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("TIPO_INCIDENCIA");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("TIPO_VEHICULO");

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ_USUARIO")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rol).HasColumnName("rol");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.RolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Rol)
                    .HasConstraintName("FK_USUARIO_ROL");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("VEHICULO");

                entity.HasIndex(e => e.Vin)
                    .HasName("UQ_VEHICULO")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Anyo).HasColumnName("anyo");

                entity.Property(e => e.Desplegado).HasColumnName("desplegado");

                entity.Property(e => e.EnServicio).HasColumnName("enServicio");

                entity.Property(e => e.Matricula)
                    .IsRequired()
                    .HasColumnName("matricula")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVehiculo)
                    .IsRequired()
                    .HasColumnName("tipoVehiculo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasColumnName("vin")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoVehiculoNavigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.TipoVehiculo)
                    .HasConstraintName("FK_VEHICULO_TIPO_VEHICULO");
            });
        }
    }
}
