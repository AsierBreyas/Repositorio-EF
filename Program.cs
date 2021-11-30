using System;
using System.Linq;

namespace EFRepaso2
{
    internal class Program
    {
        private static void Main()
        {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.
                Console.WriteLine($"Database path: {db.connString}.");
                //Delete
                db.Blogs.RemoveRange(db.Blogs);
                db.Etiqueta.RemoveRange(db.Etiqueta);
                db.EtiquetaBlog.RemoveRange(db.EtiquetaBlog);
                db.SaveChanges();
                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.Add(new Blog { Url = "http://patata.madao.com" });
                db.Add(new Blog { Url = "http://eustaquio.habichuela.com" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blogs = db.Blogs
                    .OrderBy(b => b.BlogId);
                // ,.First();
                // Update
                // Console.WriteLine("Updating the blog and adding a post");
                // blog.Url = "https://devblogs.microsoft.com/dotnet";
                // blog.Posts.Add(
                //     new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                // db.SaveChanges();
                Console.WriteLine("Inserto etiqueta");
                db.Add(new Etiqueta { Nombre = "Gatos", Descripcion = "Gaticos monos" });
                db.Add(new Etiqueta { Nombre = "Comedia", Descripcion = "Jajajaja xd" });
                db.Add(new Etiqueta { Nombre = "Informativo", Descripcion = "Informa de cosas" });
                db.Add(new Etiqueta { Nombre = "Animu", Descripcion = "Asi es, una etiqueta para ti otaku mugroso" });
                db.SaveChanges();
                var etiquetas = db.Etiqueta;
                // .OrderBy(b => b.EtiquetaId);
                // // ,.First();
                // db.Add(new BlogEtiqueta{BlogId = blog.BlogId, EtiquetaId = etiquet.EtiquetaId});
                foreach (Blog blog in blogs)
                {
                    foreach (Etiqueta etiqueta in etiquetas)
                    {
                        db.Add(new BlogEtiqueta{BlogId = blog.BlogId, EtiquetaId = etiqueta.EtiquetaId});
                    }
                }
                db.SaveChanges();
            }
        }
    }
}