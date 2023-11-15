using Benday.EasyAuthDemo.Api.DataAccess.Entities;

namespace Benday.EasyAuthDemo.Api
{
    public interface IAttributedEntity
    {
        List<EntityBase> GetAttributes();
    }
}
