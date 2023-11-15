using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Benday.EasyAuthDemo.Api.DomainModels;

namespace Benday.EasyAuthDemo.Api.Adapters
{
    public partial class ConfigurationItemAdapter :
        AdapterBase<ConfigurationItem, ConfigurationItemEntity>
    {
        protected override void PerformAdapt(
            ConfigurationItem fromValue,
            ConfigurationItemEntity toValue)
        {
            PerformAdapt(fromValue, toValue, false);
        }

        protected override void PerformAdapt(
            ConfigurationItem fromValue,
            ConfigurationItemEntity toValue, bool onlyScalarProperties)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue));
            }

            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue));
            }
            toValue.Category = fromValue.Category;
            toValue.ConfigurationKey = fromValue.ConfigurationKey;
            toValue.Description = fromValue.Description;
            toValue.ConfigurationValue = fromValue.ConfigurationValue;
            toValue.Id = fromValue.Id;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
        }

        protected override void PerformAdapt(
            ConfigurationItemEntity fromValue,
            ConfigurationItem toValue
            )
        {
            PerformAdapt(fromValue, toValue, false);
        }

        protected override void PerformAdapt(
            ConfigurationItemEntity fromValue,
            ConfigurationItem toValue, bool onlyScalarProperties
            )
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue));
            }

            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue));
            }

            toValue.Category = fromValue.Category;
            toValue.ConfigurationKey = fromValue.ConfigurationKey;
            toValue.Description = fromValue.Description;
            toValue.ConfigurationValue = fromValue.ConfigurationValue;
            toValue.Id = fromValue.Id;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;


            toValue.AcceptChanges();
        }
    }
}
