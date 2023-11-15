using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.EasyAuthDemo.UnitTests.DataAccess.Entities
{
    [TestClass]
    public class UserEntityFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private UserEntity _systemUnderTest;
        public UserEntity SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new UserEntity();
                }

                return _systemUnderTest;
            }
        }
    }
}
