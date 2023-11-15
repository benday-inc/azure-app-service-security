using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;

namespace Benday.EasyAuthDemo.Api.DataAccess.SqlServer
{
    public partial interface IConfigurationItemRepository :
        ISearchableRepository<ConfigurationItemEntity>
    {
}
}