using System;
using System.Collections.Generic;

namespace TurfTankRegistrationApplication.Helpers
{
    // Abstract class where the constructor handles the string manipulation of the JSON response
    // we receive from the Rover after requesting the Serial Number. 
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
                try //todo hvorfor er denne i en try catch blok når vi ikke håndterer catchen?
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

    // This class basically just handles the string manipulation that gets us the
    // Rover serial number from the JSON response.
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
