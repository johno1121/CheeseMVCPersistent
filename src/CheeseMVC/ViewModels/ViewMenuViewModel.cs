using System.Collections.Generic;
using CheeseMVC.Models;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public IList<CheeseMenu> Items { get; set; }
        public Menu Menu { get; set; }

    }
}