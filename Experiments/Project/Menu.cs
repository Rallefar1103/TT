using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class Menu
    {
        public string Title { get; set; }
        public List<MenuItem> MenuList = new List<MenuItem>();
        public Menu(string title)
        {
            Title = title;
        }
        public void AddItem(MenuItem menu_item)
        {
            MenuList.Add(menu_item);
        }
        public void Start(bool run_command)
        {
        }
    }
}
