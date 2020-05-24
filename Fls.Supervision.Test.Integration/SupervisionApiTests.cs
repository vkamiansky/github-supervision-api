using Fls.Supervision.Api;
using Fls.Supervision.Test.Integration.Helpers;
using Xunit;

namespace Fls.Supervision.Test.Integration
{
    public class SupervisionApiTests
    {
        [Fact]
        public async void PingTest()
        {
            var server = TestHelper.CreateTestServer(webHostBuilder => webHostBuilder.UseSupervisionApi());
            var client = TestHelper.CreateTestClient(server);
            var result = await client.GetAsync(() => TestEndpoints.PingGet);
            Assert.Equal("pong", result);
        }
    }
}
