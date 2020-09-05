using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class SomeClass
    {
        public decimal Salary = 0;
        public SomeClass(decimal salary)
        {
            Salary = salary;
        }
        // It is assumed that "AddOne" is a requirement... although a bad one.
        public int AddOne(int val)
        {
            return val + 1;
        }
        public void IncreaseSalary(decimal increase)
        {
            Salary += increase;
        }
    }
}
