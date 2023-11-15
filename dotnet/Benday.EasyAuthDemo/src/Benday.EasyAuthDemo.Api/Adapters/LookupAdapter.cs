using Benday.EasyAuthDemo.Api.DataAccess.Entities;
using Benday.EasyAuthDemo.Api.DomainModels;

namespace Benday.EasyAuthDemo.Api.Adapters
{
    public partial class LookupAdapter :
        AdapterBase<Lookup, LookupEntity>
    {
        protected override void PerformAdapt(
            Lookup fromValue,
            LookupEntity toValue)
        {
            PerformAdapt(fromValue, toValue, false);
        }

        protected override void PerformAdapt(
            Lookup fromValue,
            LookupEntity toValue, bool onlyScalarProperties)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue));
            }

            if (toValue == null)
            {
                throw new ArgumentNullException(nameof(toValue));
            }
            toValue.DisplayOrder = fromValue.DisplayOrder;
            toValue.LookupType = fromValue.LookupType;
            toValue.LookupKey = fromValue.LookupKey;
            toValue.LookupValue = fromValue.LookupValue;
            toValue.Id = fromValue.Id;
            toValue.Status = fromValue.Status;
            toValue.CreatedBy = fromValue.CreatedBy;
            toValue.CreatedDate = fromValue.CreatedDate;
            toValue.LastModifiedBy = fromValue.LastModifiedBy;
            toValue.LastModifiedDate = fromValue.LastModifiedDate;
            toValue.Timestamp = fromValue.Timestamp;
        }

        protected override void PerformAdapt(
            LookupEntity fromValue,
            Lookup toValue
            )
        {
            PerformAdapt(fromValue, toValue, false);
        }

        protected override void PerformAdapt(
            LookupEntity fromValue,
            Lookup toValue, bool onlyScalarProperties
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

            toValue.DisplayOrder = fromValue.DisplayOrder;
            toValue.LookupType = fromValue.LookupType;
            toValue.LookupKey = fromValue.LookupKey;
            toValue.LookupValue = fromValue.LookupValue;
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
