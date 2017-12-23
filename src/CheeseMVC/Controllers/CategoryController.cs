using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            var cheeseCategory = context.Categories.ToList();
            return View(cheeseCategory);
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = addCategoryViewModel.Name
                };
                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/Category");
            }
            return View(addCategoryViewModel);
        }
    }
}