using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class SomeClass
    {
        // It is assumed that "AddOne" is a requirement... although a bad one.
        public int AddOne(int val)
        {
            return Math.Abs(val) + 1;
        }
    }
}
