using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Benday.EfCore.SqlServer;

namespace Benday.EasyAuthDemo.Api.DataAccess.SqlServer
{
    public interface ILookupRepository : ISearchableRepository<LookupEntity>
    {
        IList<LookupEntity> GetAllByType(string lookupType);
    }
}
