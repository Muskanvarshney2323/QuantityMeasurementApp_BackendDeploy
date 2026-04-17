using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModel.Entities;

namespace QuantityMeasurementAppRepositoryLayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MeasurementRecord> MeasurementRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                      .HasMaxLength(50);

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(x => x.IsGoogleUser)
                      .IsRequired();

                entity.Property(x => x.CreatedAt)
                      .IsRequired();
            });

            modelBuilder.Entity<MeasurementRecord>(entity =>
            {
                entity.ToTable("MeasurementRecords");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                      .HasMaxLength(50);

                entity.Property(x => x.Timestamp)
                      .IsRequired();

                entity.Property(x => x.Operation)
                      .IsRequired();

                entity.Property(x => x.Input1Unit).HasMaxLength(50);
                entity.Property(x => x.Input1Type).HasMaxLength(50);
                entity.Property(x => x.Input2Unit).HasMaxLength(50);
                entity.Property(x => x.Input2Type).HasMaxLength(50);
                entity.Property(x => x.DesiredUnit).HasMaxLength(50);
                entity.Property(x => x.OriginalUnit).HasMaxLength(50);
                entity.Property(x => x.OriginalType).HasMaxLength(50);
                entity.Property(x => x.OutputUnit).HasMaxLength(50);
                entity.Property(x => x.OutputText).HasMaxLength(500);
                entity.Property(x => x.ErrorMessage).HasMaxLength(500);
            });
        }
    }
}