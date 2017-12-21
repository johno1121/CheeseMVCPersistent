using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.ViewModels;
using CheeseMVC.Models;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            var cheeseCategory = context.Categories.ToList();

            return View(cheeseCategory);
        }


        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
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