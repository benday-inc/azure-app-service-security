using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.EasyAuthDemo.Api.Adapters;
using Benday.EasyAuthDemo.Api.DomainModels;
using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Benday.EasyAuthDemo.Api.DataAccess.SqlServer;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.EasyAuthDemo.Api.ServiceLayers
{
    public partial class PersonService :
        CoreFieldsServiceLayerBase<Benday.EasyAuthDemo.Api.DomainModels.Person>,
        IPersonService
    {
        private IPersonRepository _Repository;
        private PersonAdapter _Adapter;
        private IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Person> _ValidatorInstance;
        private ISearchStringParserStrategy _SearchStringParser;
        
        public PersonService(
            IPersonRepository repository,
            IValidatorStrategy<Benday.EasyAuthDemo.Api.DomainModels.Person> validator,
            IUsernameProvider usernameProvider, ISearchStringParserStrategy searchStringParser) :
            base(usernameProvider)
        {
            _Repository = repository;
            _ValidatorInstance = validator;
            _SearchStringParser = searchStringParser;
            
            _Adapter = new PersonAdapter();
        }
        
        public IList<Benday.EasyAuthDemo.Api.DomainModels.Person> GetAll(
            int maxNumberOfResults = 100)
        {
            var entityResults = _Repository.GetAll(maxNumberOfResults);
            
            var returnValues = new List<Benday.EasyAuthDemo.Api.DomainModels.Person>();
            
            _Adapter.Adapt(entityResults, returnValues);
            
            BeforeReturnFromGet(returnValues);
            
            return returnValues;
        }
        
        public Benday.EasyAuthDemo.Api.DomainModels.Person GetById(int id)
        {
            var entityResults = _Repository.GetById(id);
            
            if (entityResults == null)
            {
                return null;
            }
            else
            {
                var returnValue = new Benday.EasyAuthDemo.Api.DomainModels.Person();
                
                _Adapter.Adapt(entityResults, returnValue);
                
                BeforeReturnFromGet(returnValue);
                
                return returnValue;
            }
        }
        
        public void Save(Benday.EasyAuthDemo.Api.DomainModels.Person saveThis)
        {
            if (saveThis == null)
            throw new ArgumentNullException("saveThis", "saveThis is null.");
            
            if (_ValidatorInstance.IsValid(saveThis) == false)
            {
                ApiUtilities.ThrowValidationException(saveThis, "Item is invalid.");
            }
            else
            {
                Benday.EasyAuthDemo.Api.DataAccess.Entities.PersonEntity toValue;
                
                if (saveThis.Id == 0)
                {
                    toValue = new Benday.EasyAuthDemo.Api.DataAccess.Entities.PersonEntity();
                }
                else
                {
                    toValue = _Repository.GetById(saveThis.Id);
                    
                    if (toValue == null)
                    {
                        ApiUtilities.ThrowUnknownObjectException("Person", saveThis.Id);
                    }
                }
                
                PopulateAuditFieldsBeforeSave(saveThis);
                
                
                
                _Adapter.Adapt(saveThis, toValue);
                
                _Repository.Save(toValue);
                
                PopulateFieldsFromEntityAfterSave(toValue, saveThis);
                
                
            }
        }
        
        public void DeleteById(int id)
        {
            var match = _Repository.GetById(id);
            
            if (match == null)
            {
                throw new InvalidOperationException(
                $"Could not locate an item with an id of '{id}'."
                );
            }
            else
            {
                _Repository.Delete(match);
            }
        }
        
        public IList<Benday.EasyAuthDemo.Api.DomainModels.Person> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100)
        {
            Search search = GetSimpleSearch(searchValue, maxNumberOfResults);
            
            if (sortBy != null)
            {
                search.AddSort(sortBy, sortByDirection);
            }
            
            return Search(search);
        }
        
        private Search GetSimpleSearch(string searchValue, int maxNumberOfResults)
        {
            var search = new Search();
            
            search.MaxNumberOfResults = maxNumberOfResults;
            
            var searchTokens = _SearchStringParser.Parse(searchValue);
            
            foreach (var searchToken in searchTokens)
            {
                AddSimpleSearchForValue(search, searchToken);
            }
            
            return search;
        }
        
        private void AddSimpleSearchForValue(Search search, string searchValue)
        {
            search.AddArgument(
            "FirstName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "PhoneNumber", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "EmailAddress", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Status", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "CreatedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastModifiedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            
            
        }
        
        public IList<Benday.EasyAuthDemo.Api.DomainModels.Person> Search(
            SearchMethod searchTypeFirstName = SearchMethod.Contains,
            string searchValueFirstName = null,
            SearchMethod searchTypeLastName = SearchMethod.Contains,
            string searchValueLastName = null,
            SearchMethod searchTypePhoneNumber = SearchMethod.Contains,
            string searchValuePhoneNumber = null,
            SearchMethod searchTypeEmailAddress = SearchMethod.Contains,
            string searchValueEmailAddress = null,
            SearchMethod searchTypeStatus = SearchMethod.Contains,
            string searchValueStatus = null,
            SearchMethod searchTypeCreatedBy = SearchMethod.Contains,
            string searchValueCreatedBy = null,
            SearchMethod searchTypeLastModifiedBy = SearchMethod.Contains,
            string searchValueLastModifiedBy = null,
            
            
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100)
        {
            var search = new Search();
            
            if (sortBy != null)
            {
                search.AddSort(sortBy, sortByDirection);
            }
            
            bool foundOneNonNullSearchValue = false;
            
            if (String.IsNullOrWhiteSpace(searchValueFirstName) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "FirstName", searchTypeFirstName, searchValueFirstName);
            }
            if (String.IsNullOrWhiteSpace(searchValueLastName) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "LastName", searchTypeLastName, searchValueLastName);
            }
            if (String.IsNullOrWhiteSpace(searchValuePhoneNumber) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "PhoneNumber", searchTypePhoneNumber, searchValuePhoneNumber);
            }
            if (String.IsNullOrWhiteSpace(searchValueEmailAddress) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "EmailAddress", searchTypeEmailAddress, searchValueEmailAddress);
            }
            if (String.IsNullOrWhiteSpace(searchValueStatus) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Status", searchTypeStatus, searchValueStatus);
            }
            if (String.IsNullOrWhiteSpace(searchValueCreatedBy) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "CreatedBy", searchTypeCreatedBy, searchValueCreatedBy);
            }
            if (String.IsNullOrWhiteSpace(searchValueLastModifiedBy) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "LastModifiedBy", searchTypeLastModifiedBy, searchValueLastModifiedBy);
            }
            
            
            
            var returnValues = new List<Benday.EasyAuthDemo.Api.DomainModels.Person>();
            
            if (foundOneNonNullSearchValue == true)
            {
                search.MaxNumberOfResults = maxNumberOfResults;
                
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public IList<Benday.EasyAuthDemo.Api.DomainModels.Person> Search(Search search)
        {
            var returnValues = new List<Benday.EasyAuthDemo.Api.DomainModels.Person>();
            
            if (search == null ||
            search.Arguments == null ||
            search.MaxNumberOfResults == 0)
            {
                return returnValues;
            }
            else
            {
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
    }
}

