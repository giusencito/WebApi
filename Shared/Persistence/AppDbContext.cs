


using WebApi.Domain.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Shared.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        public DbSet<Rol> Rols { get; set; }

        public DbSet<User> Users { get; set; }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Album>().HasKey(a => a.Id);
            modelBuilder.Entity<Album>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Album>().Property(a => a.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Album>().Property(a => a.Description).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Album>().HasMany(a => a.Songs).WithOne(a => a.Album).HasForeignKey(p=>p.AlbumId);

            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Song>().HasKey(a => a.Id);
            modelBuilder.Entity<Song>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Song>().Property(a => a.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Song>().Property(a => a.MusicUrl).IsRequired();
            modelBuilder.Entity<Song>().Property(a => a.Category).IsRequired().HasConversion<string>();


            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(a => a.Username).IsRequired();
            modelBuilder.Entity<User>().Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(a => a.Email).IsRequired();
            modelBuilder.Entity<User>().Property(a => a.PasswordHash).IsRequired();

            modelBuilder.Entity<User>()
    .HasMany(p => p.roles).WithMany( r=> r.users)
    .UsingEntity<Dictionary<string, object>>(
        "UserRoles",
        j => j.HasOne<Rol>().WithMany().HasForeignKey("RoleId"),
        j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
    );




            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Rol>().HasKey(a => a.Id);
            modelBuilder.Entity<Rol>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Rol>().Property(a => a.Rolname).IsRequired().HasConversion<string>();








        }


    }
}
