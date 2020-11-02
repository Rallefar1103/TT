using NUnit.Framework;
using Moq;
using NSubstitute;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(2,3,5)]
        [TestCase(1, 7, 8)]
        [TestCase(-121, 21, -100)]
        public void Test1(int arr_mock, int arr_ec, int expected)
        {
            IEmptyClass2 e2 = Substitute.For<IEmptyClass2>();
            EmptyClass emptC = new EmptyClass(e2);
            EmptyClass2 ec2 = new EmptyClass2();
            EmptyClass emptCb = new EmptyClass(ec2);



            e2.get_empty_id().Returns(22);

            Assert.That(emptC.get_my_id(), Is.EqualTo(-5));
        }
    }
}