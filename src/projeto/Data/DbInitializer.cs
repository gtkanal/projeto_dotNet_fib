using projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //Verifica se o banco de dados já foi criado
            //Se ele não tiver sido criado, cria automaticamente
            context.Database.EnsureCreated();

            //se o livro tiver registros, não é necessário inserir novos dados
            if (context.Livro.Any())
            {
                return; // DB possui registros
            }

            var livros = new Livro[]
            {
                new Livro { Titulo = "PHP para quem conhece PHP", Quantidade = 10},     
                new Livro { Titulo = "Internet das coisas com ESP8266, Arduino e Raspberry", Quantidade = 10},  
                new Livro { Titulo = "Gamification em Help Desk e Service Desk", Quantidade = 10},  
                new Livro { Titulo = "Desenvolvimento de Jogos Mobile com HTML5", Quantidade = 10},
            };

            foreach (Livro l in livros)
            {
                context.Livro.Add(l);
            }

            var autores = new Autor[]
            {
                new Autor { Nome = "Sérgio de Oliveira" },
                new Autor { Nome = "Renato da Silva"},
                new Autor { Nome = "Paulo Sérgio Travolla"},
                new Autor { Nome = "Juliano Niederauer"},
                new Autor { Nome = "Roberto Cohen"},
                new Autor { Nome = "Chris McNab"},
                new Autor { Nome = "Luiz Fernando Estevarengo"}
            };

            foreach (Autor a in autores)
            {
                context.Autor.Add(a);
            }

            var usuarios = new Usuario[]
            {
                new Usuario { Nome = "Teste", Email = "teste@teste.com", senha = "abc123" }
            };

            foreach (Usuario a in usuarios)
            {
                context.Usuario.Add(a);
            }

            context.SaveChanges();

        }

    }

}
