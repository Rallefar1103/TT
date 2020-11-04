using System;
namespace TurfTankRegistrationApplication.Model
{

    public interface IScanableSticker
    {
        
        string Scan();
        string ManualInput();
        string Data { get; set; }
        bool IsDiscarded { get; set; }
        Component LinkedTo { get; set; }

    }

    public abstract class ScanableSticker : IScanableSticker
    {
        public string Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Component LinkedTo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsDiscarded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public bool isDiscarded = false;

        public string Scan()
        {
            throw new NotImplementedException();
        }

        public string ManualInput()
        {
            throw new NotImplementedException();
        }
    }
}
