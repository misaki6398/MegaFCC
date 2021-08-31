using MegaTFLT.Services;
using MegaTFLT.Utilitys;
using NUnit.Framework;

namespace MegaTFLT.Tests.Services
{
    public class MqServiceTest
    {
        private MqSerivce _mqSerivce;

        [SetUp]
        public void Setup()
        {
            _mqSerivce = new MqSerivce(ConfigUtility.MqModel);
        }

        [Test]
        [TestCase("31TEST.QL")]
        public void TestGetCurrentDepth(string queueName)
        {
            var result = _mqSerivce.GetCurrentDepth(queueName);
            Assert.AreEqual(result, 0);
        }

        [Test]
        [TestCase("31TEST")]
        public void TestGetCurrentDepthError(string queueName)
        {
            var result = _mqSerivce.GetCurrentDepth(queueName);
            Assert.AreEqual(result, -1);
        }

        [Test]
        [TestCase("31TEST.QL", "TEST")]
        public void TestPutMessage(string queueName, string messageString)
        {
            try
            {
                _mqSerivce.PutMessage(queueName, messageString);
                return;
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase("31TEST.QL")]
        public void TestReceiveMessage(string queueName)
        {
            try
            {
                MqSerivce mqSerivce = new MqSerivce(ConfigUtility.MqModel);
                var result = mqSerivce.ReceiveMessage(queueName);
                mqSerivce.Disconnect();
                Assert.AreEqual(result, "TEST");
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}


