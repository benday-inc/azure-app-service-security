using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.EasyAuthDemo.Api.DomainModels;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.EasyAuthDemo.Api.ServiceLayers
{
    public partial interface IPersonService : IServiceLayer<Person>
    {
        IList<Person> Search(
            SearchMethod searchTypeFirstName = SearchMethod.Contains,
            string searchValueFirstName = null,
            SearchMethod searchTypeLastName = SearchMethod.Contains,
            string searchValueLastName = null,
            SearchMethod searchTypePhoneNumber = SearchMethod.Contains,
            string searchValuePhoneNumber = null,
            SearchMethod searchTypeEmailAddress = SearchMethod.Contains,
            string searchValueEmailAddress = null,
            SearchMethod searchTypeId = SearchMethod.Skip,
            int searchValueId = 0,
            SearchMethod searchTypeStatus = SearchMethod.Contains,
            string searchValueStatus = null,
            SearchMethod searchTypeCreatedBy = SearchMethod.Contains,
            string searchValueCreatedBy = null,
            SearchMethod searchTypeLastModifiedBy = SearchMethod.Contains,
            string searchValueLastModifiedBy = null,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        IList<Benday.EasyAuthDemo.Api.DomainModels.Person> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        
    }
}