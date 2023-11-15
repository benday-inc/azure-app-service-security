using Benday.EasyAuthDemo.Api.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.EasyAuthDemo.UnitTests.Utilities;
using Benday.EasyAuthDemo.WebUi.Models;
using Benday.EasyAuthDemo.WebUi.Models.Adapters;

namespace Benday.EasyAuthDemo.UnitTests.ViewModels.Adapters
{
    [TestClass]
    public class UserAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private UserEditorViewModelAdapter _systemUnderTest;
        public UserEditorViewModelAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new UserEditorViewModelAdapter();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void AdaptUserFromViewModelsToModels()
        {
            // arrange
            var fromValues = UserViewModelTestUtility.CreateEditorViewModels();
            
            var allValuesCount = fromValues.Count;
            
            var toValues = new List<Benday.EasyAuthDemo.Api.DomainModels.User>();
            
            // act
            SystemUnderTest.Adapt(fromValues, toValues);
            
            // assert
            Assert.AreEqual<int>(allValuesCount, toValues.Count, "Count was wrong.");
        }
        
        [TestMethod]
        public void AdaptUserFromViewModelToModel()
        {
            // arrange
            var fromValue = UserViewModelTestUtility.CreateEditorViewModel();
            var toValue = new Benday.EasyAuthDemo.Api.DomainModels.User();
            
            // act
            SystemUnderTest.Adapt(fromValue, toValue);
            
            // assert
            UserViewModelTestUtility.AssertAreEqual(fromValue, toValue);
        }
        
        [TestMethod]
        public void AdaptUserFromModelToViewModel()
        {
            // arrange
            var fromValue = UserTestUtility.CreateModel();
            var toValue = new Benday.EasyAuthDemo.WebUi.Models.UserEditorViewModel();
            
            // act
            SystemUnderTest.Adapt(fromValue, toValue);
            
            // assert
            UserViewModelTestUtility.AssertAreEqual(fromValue, toValue);
        }
    }
}