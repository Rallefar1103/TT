using System;

namespace UnitTest
{
    public class EmptyClass
    {
        public IEmptyClass2 _empty2;

        public EmptyClass(IEmptyClass2 empty2)
        {
            this._empty2 = empty2;
            id = get_my_id();
        }
        public int id { get; set; } = 5;
        public int get_my_id()
        {
            return _empty2.get_empty_id();
        }
    }

  
    
}
