using Benday.EasyAuthDemo.Api.DomainModels;
using Benday.EasyAuthDemo.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benday.EasyAuthDemo.UnitTests.DomainModels
{
    [TestClass]
    public partial class PersonFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private Person _systemUnderTest;
        public Person SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new Person();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void Person_VerifyDomainModelBaseOperations()
        {
            var instance = PersonTestUtility.CreateModel(false);
            
            instance.AcceptChanges();
            
            var tester = new DomainModelFieldTester<Person>(instance);
            
            tester.RunChangeTrackingTestsForValueTypeProperties();
        }
    }
}