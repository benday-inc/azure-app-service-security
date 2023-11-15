using Benday.EasyAuthDemo.Api.Security;

namespace Benday.EasyAuthDemo.UnitTests
{
    public class FakeRouteDataAccessor : IRouteDataAccessor
    {
        public bool WasGetIdCalled { get; set; }
        public string GetIdReturnValue { get; set; }

        public string GetId()
        {
            WasGetIdCalled = true;
            return GetIdReturnValue;
        }
    }
}
