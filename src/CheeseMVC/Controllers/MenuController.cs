using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            this.context = dbContext;
        }

        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu");
            }

            return View(addMenuViewModel);
        }

        public IActionResult ViewMenu(int id)
        {
            //displays every cheese in the particular menu
            //retrieves a list of cheese menus, and include every property
            //stored in the database
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                //filters down to only the items we want
                .Where(cm => cm.MenuID == id)
                .ToList();

            //we also want the menu object itself
            //we retrieve it using the given ID
            Menu menu = context.Menus.FirstOrDefault(m => m.ID == id);

            //we are using the items and menu we found above to 
            //build this ViewMenuViewModel and pass it into the View
            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewModel);
        }

        public IActionResult AddItem(int id)
        {
            //retrieve the menu with the given ID via context
            //list of all cheeses in the system
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(menu, cheeses));
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuID;

                //can't add a cheese to a menu that already exists
                //can't have two rows with the same primary key
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    //first look for cheeses with the given ID
                    .Where(cm => cm.CheeseID == cheeseID)
                    //out of those cheeses, then look for menus with the given ID
                    .Where(cm => cm.MenuID == menuID).ToList();

                //if they didn't already exist, make a new object from the pair of IDs
                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu

                    {
                        //these values are in the ViewModel
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };

                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();
                }

                //return Redirect(string.Format("/Menu"));
                return Redirect(string.Format("/Menu/ViewMenu/" + addMenuItemViewModel.MenuID));
            }

            return View(addMenuItemViewModel);
        }

    }
}