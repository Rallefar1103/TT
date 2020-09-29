using System;
using System.Collections.Generic;
using System.Text;

namespace MartinKursus3.Models
{
    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public Car()
        {
            Make = "Maker";
            Model = "Model";
            Price = 0.00;
        }
        public Car(string make, string model, double price)
        {
            Make = make;
            Model = model;
            Price = price;
        }
    }
}
