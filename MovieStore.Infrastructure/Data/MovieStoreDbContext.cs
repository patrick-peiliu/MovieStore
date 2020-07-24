using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;

namespace MovieStore.Infrastructure.Data
{    
    // Install all the EF Core libraries using Nuget package Manger
    // Create a class that inherits from DbContext class
    // DbContext kinda represents your Database
    // Create a connection string which EF is gonna use to create/access the Database, should include server name, Database Name and any credentials
    // Create an Entity Class, Genre table
    // Make sure to add your Entity class as a DbSet property inside your DbContext class
    // In EF we have thing called Migrations, we are gonna use Migrations to create our Database
    // We need to add Migration commands to Create the tables, database etc
    // When running Migration commands, make sure the project selected is the one project which has DbContext class
    // Make sure you add reference for library that has DbContext to your startup project, in this case MVC
    // Tell MVC project that we are using Entity Framework in startup file
    // Add DbContext options as constructor parameter for our DbContext
    // 'Add-Migration MigrationName', make sure your migration names are meaningful, don't use names such as xyz, abc, migration1 like that
    // Make sure you have Migrations folder created, and check the created migration file
    // After Creating Migration file and verifying it we need to use update-database command
    // Whenever changes on model / entity, you are required to add a new Migration
    // With code-first approach, never change the db directly, always change the entities
    // using data annotations and Fluent API and add new migration then update db

    // In EF we have 2 ways to create entities and model db using code-first approach
    // 1. Data annotations which is nothing but bunch of c# attributes that we can use
    // 2. Fluent API is more syntax and more powerful and usually uses lambda
    // ** Combine both data annotations and Fluent API

    public class MovieStoreDbContext : DbContext
    {
        // Multiple DbSets, all the DbSets you create are gonna reside in your DbContext class
        // This DbSet, is gonna represent out Table based on Entity class which is Genre in this case
        // constructor
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> modelBuilder)
        {
            modelBuilder.ToTable("Review");
            modelBuilder.HasKey(r => new { r.MovieId, r.UserId });
            modelBuilder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
            //modelBuilder.HasOne(re => re.User).WithMany
            //    (r => r.Reviews).HasForeignKey(re => re.UserId);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> modelBuilder)
        {
            modelBuilder.ToTable("Purchase");
            modelBuilder.Property(p => p.PurchaseNumber).IsRequired();
            modelBuilder.Property(p => p.TotalPrice).IsRequired().HasColumnType("decimal(5, 2)");
            modelBuilder.Property(p => p.PurchaseDateTime).IsRequired();
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> modelBuilder)
        {
            modelBuilder.ToTable("UserRole");
            modelBuilder.HasKey(ur => new { ur.UserId, ur.RoleId});
            modelBuilder.HasOne(ur => ur.User).WithMany
                (r => r.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.HasOne(ur => ur.Role).WithMany
                (r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> modelBuilder)
        {
            modelBuilder.ToTable("MovieCast");
            modelBuilder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
            modelBuilder.Property(mc => mc.Character).HasMaxLength(450);
            modelBuilder.HasOne(mc => mc.Movie).WithMany
                (c => c.MovieCasts).HasForeignKey(mc => mc.MovieId);
            modelBuilder.HasOne(mc => mc.Cast).WithMany
                (c => c.MovieCasts).HasForeignKey(mc => mc.CastId);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> modelBuilder)
        {
            modelBuilder.ToTable("Cast");
            modelBuilder.Property(c => c.Name).HasMaxLength(128);
            modelBuilder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder)
        {
            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            // For genre table, FK is MovieId
            modelBuilder.HasOne(mg => mg.Movie).WithMany
                (g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            // For Movie Table, FK is GenreId
            modelBuilder.HasOne(mg => mg.Genre).WithMany
                (g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);

        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> modelBuilder)
        {
            modelBuilder.ToTable("Trailer");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).HasMaxLength(2084);
            modelBuilder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> modelBuilder)
        {
            // we can use Fluent API COnfigurations to model our tables         
            modelBuilder.ToTable("Movie");
            modelBuilder.HasKey(m => m.Id);
            modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            modelBuilder.Property(m => m.Overview).HasMaxLength(4096);
            modelBuilder.Property(m => m.Tagline).HasMaxLength(512);
            modelBuilder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.PosterUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            modelBuilder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            modelBuilder.Property(m => m.Budget).HasColumnType("decimal(18, 2)");
            modelBuilder.Property(m => m.Revenue).HasColumnType("decimal(18, 2)");
            modelBuilder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            // we don't want to Create Rating Column
            // but we want C# rating property in our Entity so that we can show Movie rating in the UI

            modelBuilder.Ignore(m => m.Rating);
        }

        

    }
}