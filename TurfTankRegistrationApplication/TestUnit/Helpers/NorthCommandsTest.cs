using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Helpers;
using NUnit.Framework;

namespace TestUnit.Helpers
{
    [TestFixture]
    public class NorthCommandsTest
    {
        [TestCase("Testing that the JSON parser can parse the JSON response from Rover")]
        public void SOSVER_jsonResponse_ShouldParseFromJsonToCorrectString(string desc)
        {
            // arrange
            string testresponse = "$SOSVER,0,SMARTOS 1.3.5 EN,0917W - SRTK0128 * 0B\n";
            string expected = "0917W - SRTK0128";


            // act
            SOSVER testObject = new SOSVER(testresponse);
            string actual = testObject.SerialNumber;

            // assert
            Assert.AreEqual(expected, actual, desc);
        }
    }
}
