﻿using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categorialAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categorialAtual = "Todos os lanches";
            }
            else
            {
                if(string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                    .OrderBy(l => l.LancheName);
                }
                else
                {
                    lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                    .OrderBy(l => l.LancheName);
                }
                categorialAtual = categoria;
            }
            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categorialAtual
            };

            return View(lanchesListViewModel);
        }
    }
}
