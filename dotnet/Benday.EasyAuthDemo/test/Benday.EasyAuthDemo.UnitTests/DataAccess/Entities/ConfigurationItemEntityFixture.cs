using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.EasyAuthDemo.UnitTests.DataAccess.Entities
{
    [TestClass]
    public class ConfigurationItemEntityFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private ConfigurationItemEntity _systemUnderTest;
        public ConfigurationItemEntity SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new ConfigurationItemEntity();
                }

                return _systemUnderTest;
            }
        }
    }
}
