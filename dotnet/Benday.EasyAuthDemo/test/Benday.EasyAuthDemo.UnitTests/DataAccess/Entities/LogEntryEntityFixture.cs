using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.EasyAuthDemo.UnitTests.DataAccess.Entities
{
    [TestClass]
    public class LogEntryEntityFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private LogEntryEntity _systemUnderTest;
        public LogEntryEntity SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new LogEntryEntity();
                }

                return _systemUnderTest;
            }
        }
    }
}
