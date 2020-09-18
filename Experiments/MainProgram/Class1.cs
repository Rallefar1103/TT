using System;
using System.Security.Principal;

namespace MainProgram
{
    public class Class1
    {
        private static string priv { get { return "Private!"; } }
        protected static string prot { get { return "Protected!"; } }
        public static string publ { get { return "Public!"; } }
        internal static string inte { get { return "Internal"; } }
        protected internal static string prin { get { return "Protected Internal"; } }
        private protected static string prpr { get { return "Private Protected"; } }
        public static void WriteTextStaticParent()
        {
            Console.WriteLine("I'm the Static Parent Class");
            Console.WriteLine(publ);
            Console.WriteLine(priv);
            Console.WriteLine(prot);
            Console.WriteLine(inte);
            Console.WriteLine(prin);
            Console.WriteLine(prpr);
        }
    }
    public class Class2 : Class1
    {
        public static void WriteTextStaticChild()
        {
            Console.WriteLine("Im the Static Child Class");
            Console.WriteLine(publ);
            Console.WriteLine(prot);
            Console.WriteLine(prin);
            Console.WriteLine(inte);
            Console.WriteLine(prpr);
            // Console.WriteLine(priv); Can't print Private!
        }

    }
}
