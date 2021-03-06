﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using projeto.Data;

namespace BibliotecaMVC.Utils
{
    public class Listagens
    {
        private readonly ApplicationDbContext _context;
        public Listagens(ApplicationDbContext context)
        {
            this._context = context;
        }
        public SelectList Autores()
        {
            var qry = from a in _context.Autor.AsNoTracking()
                      orderby a.Nome
                      select new
                      {

                          a.AuthorID,
                          a.Nome
                      };
            return new SelectList(qry.ToList(), "AutorID", "Nome");
        }
        public List<CheckBoxItemList> AutoresCheckBox()
        {
            var qry = from a in _context.Autor.AsNoTracking()
                      orderby a.Nome

                      select new CheckBoxItemList

                      {
                          Value = a.AuthorID,
                          Text = a.Nome

                      };
            return qry.ToList();
        }

        public SelectList Livros()
        {
            var qry = from a in _context.Livro.AsNoTracking()
                      orderby a.Titulo
                      select new
                      {

                          a.LivroID,
                          a.Titulo
                      };
            return new SelectList(qry.ToList(), "LivroID", "Titulo");
        }
        public List<CheckBoxItemList> LivrosCheckBox()
        {
            var qry = from a in _context.Livro.AsNoTracking()
                      orderby a.Titulo

                      select new CheckBoxItemList

                      {
                          Value = a.LivroID,
                          Text = a.Titulo

                      };
            return qry.ToList();
        }
    }
}