using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TurfTankRegistration.Models
{
    public abstract class Employee
    {
        /*
         An Employee is the abstract class that all Users of the program should derive from.
         */
        // Properties
        public string Username { get; set; }
        protected string Password 
        { 
            get 
            {
                // Some Robustness and De-Salting
                return Password;
            }
            set
            {
                string PreSaltPW = value;
                // Some Salt Robustness
                Password = PreSaltPW;
            }
        }
        protected string Salt { get; set; }
        // Initializers
        protected Employee()
        {
            Username = "";
            Password = "";
        }
        protected Employee(string username, string password)
        {
            Username = username;
            Password = password;
        }
        // Methods
        
        public string Login()
        {
            var rxcui = "198440";
            var request = System.Net.HttpWebRequest.Create(string.Format(@"https://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", rxcui));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (System.IO.StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                        return "";
                    }
                    else
                    {
                        return Username + Password + content;
                    }
                }
            }
        }
    }
}
