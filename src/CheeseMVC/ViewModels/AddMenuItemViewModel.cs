using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        //used for submission of form
        public int CheeseID { get; set; }
        public int MenuID { get; set; }

        //used to display/render the select box items
        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; }


        public AddMenuItemViewModel() { }

        //used for rendering the form with the correct select list items
        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Cheeses = new List<SelectListItem>();

            foreach (var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }

            Menu = menu;
        }


    }
}