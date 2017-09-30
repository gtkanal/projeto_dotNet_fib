using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;
using BibliotecaMVC.Utils;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace projeto.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IHostingEnvironment _hostingEnvironment;

        public LivrosController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: Livros
        public async Task<IActionResult> Index(string filtroPesquisa, string filtroQtdPesquisa, string ordenacaoTitulo, string ordencaoQtd,string ordenacaoFiltroAtual)
        {
            ViewBag.filtroPesquisa = filtroPesquisa;
            ViewBag.filtroQtdPesquisa = filtroQtdPesquisa;
            ViewBag.TituloSortParam = String.IsNullOrEmpty(ordenacaoTitulo) ? "titulo_desc" : "";
            ViewBag.QuantidadeSortParam = String.IsNullOrEmpty(ordencaoQtd) ? "quantidade_desc" : "";
            ViewBag.ordenacaoFiltroAtual = String.IsNullOrEmpty(ordenacaoFiltroAtual);

            var livros = _context.Livro.AsNoTracking() ;

            if (!String.IsNullOrEmpty(filtroPesquisa))
            {

                livros = livros.Where(s => s.Titulo.ToUpper().Contains(filtroPesquisa.ToUpper()));

            }


            int quantidade = 0;

            if (!String.IsNullOrEmpty(filtroQtdPesquisa) && Int32.TryParse(filtroQtdPesquisa, out quantidade))
            {

                livros = livros.Where(s => s.Quantidade.Equals(quantidade));

            }

            if (ordenacaoFiltroAtual == "titulo")
            {
                switch (ViewBag.TituloSortParam as string)
                {
                    case "titulo_desc":
                        livros = livros.OrderByDescending(t => t.Titulo);
                        break;
                    default:
                        livros = livros.OrderBy(t => t.Titulo);
                        break;
                }
            }
            else if (ordenacaoFiltroAtual == "quantidade") {
                switch (ViewBag.QuantidadeSortParam as string)
                {
                    case "quantidade_desc":
                        livros = livros.OrderByDescending(x => x.Quantidade);
                        break;
                    default:
                        livros = livros.OrderBy(x => x.Quantidade);
                        break;
                }
            }

        

            
            return View(await livros.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.AsNoTracking()
                                                .Include(l => l.AutorLivros)
                                                .ThenInclude(li => li.Autores)
                                                .SingleOrDefaultAsync(m => m.LivroID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewBag.Autores = new Listagens(_context).AutoresCheckBox();
            return View(new Livro());
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroID,Foto,Quantidade,Titulo, AutorUnico")] Livro livro, string[] selectedAutores, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (selectedAutores != null)
                {
                    livro.AutorLivros = new List<AutorLivro>();
                    foreach (var idAutor in selectedAutores)
                        
                        livro.AutorLivros.Add(new AutorLivro()
                        {
                            AutorID = int.Parse(idAutor),
                            Livros = livro
                        });
                }
                _context.Add(livro);

                await _context.SaveChangesAsync();

                livro.Foto = await RealizarUploadImagens(files, livro.LivroID);


                _context.Update(livro);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoresAux = new Listagens(_context).AutoresCheckBox();

            var livro = await _context.Livro.Include(l => l.AutorLivros).SingleOrDefaultAsync(m => m.LivroID == id);

            autoresAux.ForEach(a => a.Checked = livro.AutorLivros.Any(l => l.AutorID == a.Value));
            ViewBag.Autores = autoresAux;
            
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivroID,Foto,Quantidade,Titulo")] Livro livro, string[] selectedAutores, List<IFormFile> files)
        {
            if (id != livro.LivroID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var livroAutores =_context.AutorLivro.AsNoTracking().Where(la => la.LivroID == livro.LivroID);
                    _context.AutorLivro.RemoveRange(livroAutores);

                    await _context.SaveChangesAsync();

                    if (selectedAutores != null)
                    {
                        livro.AutorLivros = new List<AutorLivro>();
                        foreach (var idAutor in selectedAutores)
                            livro.AutorLivros.Add(new AutorLivro()
                            {
                                AutorID = int.Parse(idAutor),
                                Livros = livro
                            });
                    }

                    livro.Foto = await RealizarUploadImagens(files, livro.LivroID);

                    _context.Update(livro);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.LivroID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.SingleOrDefaultAsync(m => m.LivroID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livro.SingleOrDefaultAsync(m => m.LivroID == id);
            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.LivroID == id);
        }

        private async Task<string> RealizarUploadImagens(List<IFormFile> files, int idLivro)
        {
            // Verifica se existem arquivos selecionados
            if (files.Count > 0)
            {
                // Variável para armazenar o caminho de upload das imagens
                var pathUpload = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                // Se o caminho não existe então cria
                if (!Directory.Exists(pathUpload))
                    Directory.CreateDirectory(pathUpload);

                // Para cada arquivo faça
                foreach (var file in files)
                {
                    // Verifica se o arquivo possui informação
                    if (file.Length > 0)
                    {
                        // Concatena o nome do arquivo
                        var nomeArquivo = "livro_" + idLivro + Path.GetExtension(file.FileName);

                        // Concatena o caminho do arquivo
                        var pathFile = Path.Combine(pathUpload, nomeArquivo);

                        // Realiza a cópia
                        using (var fileStream = new FileStream(pathFile, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                        }
                        // Retorna o caminho do arquivo que será salvo
                        // no banco de dados
                        return "uploads//" + Path.GetFileName(pathFile);
                    }
                }
            }
            return null;
        }
    }
}
