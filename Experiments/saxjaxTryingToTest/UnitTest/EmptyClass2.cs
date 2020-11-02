using System;
namespace UnitTest
{
   public interface IEmptyClass2
    {
        
        public int id { get; set; }
        public int get_empty_id();
         
        
    }

    public class EmptyClass2 : IEmptyClass2
    {
        public EmptyClass2()
        {

        }

        public int id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int get_empty_id()
        {
            return 20;
        }
    }
}
