using Benday.EasyAuthDemo.Api.DomainModels;

namespace Benday.EasyAuthDemo.UnitTests.Fakes.Validation
{
    public class FakeValidatorStrategy<T> : IValidatorStrategy<T>
    {
        public bool IsValidReturnValue { get; set; }

        public bool IsValid(T validateThis)
        {
            return IsValidReturnValue;
        }
    }
}
