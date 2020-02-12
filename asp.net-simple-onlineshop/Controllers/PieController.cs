using System;
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

        /*public ViewResult List()
        {
            // using viewBag 
            // ViewBag.CurrentCategory = "Cheese Cakes";
            // using viewModels
            PiesListViewModel piesListViewModel = new PiesListViewModel();
            piesListViewModel.Pies = _pieRepository.AllPies;

            // return view with data of all pies
            return View(piesListViewModel);
        }*/

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            } else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(p => p.CategoryName == category)?.CategoryName;
            }
            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }
    }
}
