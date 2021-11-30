using System;
using System.Collections.Generic; 
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace EFRepaso2
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Etiqueta> Etiqueta {get; set;}

        public DbSet<BlogEtiqueta> EtiquetaBlog {get; set;}
        public string connString { get; private set; }

        public BloggingContext()
        {
            connString = $"Server=(localdb)\\mssqllocaldb;Database=EFPrueba2;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(connString);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogEtiqueta>().HasKey(be => new
            {
                be.BlogId,
                be.EtiquetaId
            });
        }
    }
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
        public List<BlogEtiqueta> BlogsEtiquetados { get; } = new List<BlogEtiqueta>();

        public override string ToString() => $"{BlogId}: {Url} ({Posts.Count} Posts)";
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
    public class Etiqueta{
        public int EtiquetaId {get; set;}
        public string Nombre {get; set;}
        public string Descripcion {get; set;}
         public override string ToString() => $"{EtiquetaId}: {Nombre}: {Descripcion} ";
    }
    public class BlogEtiqueta{
       [Key, Column(Order = 0)]
        public int BlogId { get; set; }
        [Key, Column(Order = 1)]
        public int EtiquetaId { get; set; }

        public Blog Blog { get; set; }
        public Etiqueta Etiqueta { get; set; }
        public override string ToString() => $"{BlogId}x{EtiquetaId}";
    }
}