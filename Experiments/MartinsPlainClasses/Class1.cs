using System;
using System.Security.Principal;

namespace MartinsPlainClasses
{
    public class Class1
    {
        private static string priv { get { return "Private!"; } }
        protected static string prot { get { return "Protected!"; } }
        public static string publ { get { return "Public!"; } }
        internal static string inte { get { return "Internal"; } }
        protected internal static string prin { get { return "Protected Internal"; } }
        private protected static string prpr { get { return "Private Protected"; } }
        public static void WriteText()
        {
            Console.WriteLine(publ);
            Console.WriteLine(priv);
            Console.WriteLine(prot);
            Console.WriteLine(inte);
            Console.WriteLine(prin);
            Console.WriteLine(prpr);
        }
    }
}
