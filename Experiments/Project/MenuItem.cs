using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public MenuItem(string title, string content)
        {
            Title = title;
            Content = content;
        }
        public void Select()
        {

        }
    }
}
