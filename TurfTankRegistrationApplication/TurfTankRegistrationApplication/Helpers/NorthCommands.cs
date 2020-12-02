using System;
using System.Collections.Generic;

namespace TurfTankRegistrationApplication.Helpers
{
    public abstract class NorthCommands
    {
        public string RawInput { get; set; }
        public string[] response { get; set; }
        public NorthCommands(string rawInput)
        {
            RawInput = rawInput;
            if (RawInput == null || RawInput == " ")
            {
                response = new string[] { " ", " ", " ", " ", " ", " ", " " };

            } else
            {
                int RawInpuLength = RawInput.Length;
                try
                {
                    RawInput = RawInput.Substring(0, RawInpuLength - 3);
                    response = RawInput.Split(',');
                } catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class SOSVER : NorthCommands
    {

        public string SerialNumber
        {
            get { return response[3].Split('*')[0].Trim(); }
        }

        public SOSVER(string input) : base(rawInput: input)
        {

        }
    }
}
