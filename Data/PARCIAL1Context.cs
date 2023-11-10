using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIMERA_API.Data.Models;

namespace PRIMERA_API.Data;

public partial class PARCIAL1Context : DbContext
{
    public PARCIAL1Context()
    {
    }

    public PARCIAL1Context(DbContextOptions<PARCIAL1Context> options) : base(options)
    {
    }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Tipovehiculo> TipoVehiculo { get; set; }
    public DbSet<Alquiler> Alquiler { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=UNIVERSIDAD\\SQLEXPRESS;Database=RENTACAR;Integrated Security=True;Encrypt=False;");

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteID);

                entity.Property(e => e.ClienteID).HasColumnName("ClienteID");

                // Mapeo del campo "Nombre"
                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .HasMaxLength(255) // Puedes ajustar la longitud máxima según tus requisitos
                    .IsRequired(); // Si el campo es obligatorio

                // Mapeo del campo "Email"
                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(255) // Ajusta la longitud máxima según tus necesidades
                    .IsRequired(); // Si el campo es obligatorio

                // Mapeo del campo "Telefono"
                entity.Property(e => e.Telefono)
                    .HasColumnName("Telefono")
                    .HasMaxLength(20) // Ajusta la longitud máxima según tus requisitos
                    .IsRequired(false); // Puede ser opcional, ya que lo marqué como "string?"
            });
            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.HasKey(e => e.AlquilerID);

                entity.Property(e => e.AlquilerID).HasColumnName("AlquilerID");

                entity.Property(e => e.ClienteID)
                    .HasColumnName("ClienteID")
                    .IsRequired();

                entity.Property(e => e.TipoVehiculoID)
                    .HasColumnName("TipoVehiculoID")
                    .IsRequired();

                entity.Property(e => e.FechaInicio)
                    .IsRequired();

                entity.Property(e => e.FechaFin)
                    .IsRequired();

                entity.Property(e => e.MontoCobro)
                    .HasColumnType("decimal(18, 2)")
                    .IsRequired();

                entity.HasOne(e => e.Cliente)
                    .WithMany()
                    .HasForeignKey(e => e.ClienteID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tipovehiculo)
                    .WithMany()
                    .HasForeignKey(e => e.TipoVehiculoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Tipovehiculo>(entity =>

            {
                entity.HasKey(e => e.TipoVehiculoID);

                entity.Property(e => e.TipoVehiculoID).HasColumnName("TipoVehiculoID");

                // Mapeo del campo "Nombre"
                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .HasMaxLength(255) // Puedes ajustar la longitud máxima según tus requisitos
                    .IsRequired(); // Si el campo es obligatorio

                // Mapeo del campo "TarifaPorDia"
                entity.Property(e => e.TarifaPorDia)
                    .HasColumnName("TarifaPorDia")
                    .HasColumnType("decimal(18, 2)"); // Ajusta la precisión y escala según tus necesidades
            });
            OnModelCreatingPartial(modelBuilder);
     }
     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
