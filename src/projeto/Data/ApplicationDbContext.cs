using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projeto.Models;

namespace projeto.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<AutorLivro>()
                .HasKey(al => new { al.AutorID, al.LivroID });

            builder.Entity<AutorLivro>()
                .HasOne(al => al.Autores)
                .WithMany(a => a.AutorLivros)
                .HasForeignKey(al => al.AutorID);

            builder.Entity<AutorLivro>()
                .HasOne(al => al.Livros)
                .WithMany(l => l.AutorLivros)
                .HasForeignKey(al => al.LivroID);


            builder.Entity<LivroEmprestimo>()
               .HasKey(le => new { le.LivroID, le.EmprestimoID });

            builder.Entity<LivroEmprestimo>()
                .HasOne(le => le.Livros)
                .WithMany(li => li.LivroEmprestimos)
                .HasForeignKey(le => le.LivroID);

            builder.Entity<LivroEmprestimo>()
                .HasOne(le => le.Emprestimos)
                .WithMany(e => e.LivroEmprestimos)
                .HasForeignKey(le => le.EmprestimoID);

            base.OnModelCreating(builder);
        }

        public DbSet<Livro> Livro { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Autor> Autor { get; set; }

        public DbSet<Emprestimo> Emprestimo { get; set; }

        public DbSet<AutorLivro> AutorLivro{ get; set; }
    }
}
