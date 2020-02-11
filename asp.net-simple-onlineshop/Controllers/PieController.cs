﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net_simple_onlineshop.Models;
using asp.net_simple_onlineshop.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asp.net_simple_onlineshop.Controllers
{
    public class PieController : Controller
    {        

        //local variables with interface
        private readonly IPieRepository _pieRepository; 
        private readonly ICategoryRepository _categoryRepository;


        // Constructor
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository) {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            // using viewBag 
            // ViewBag.CurrentCategory = "Cheese Cakes";
            // using viewModels
            PiesListViewModel piesListViewModel = new PiesListViewModel();
            piesListViewModel.Pies = _pieRepository.AllPies;

            // return view with data of all pies
            return View(piesListViewModel);
        }
    }
}