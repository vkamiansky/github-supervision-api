using System;
using System.Text.Json;
using Fls.Supervision.Api;
using Fls.Supervision.Api.Commands;
using Fls.Supervision.Test.Integration.Helpers;
using Xunit;
using System.IO;
using System.Net;

namespace Fls.Supervision.Test.Integration
{
    public class SupervisionApiTests
    {
        [Fact]
        public async void PingTest()
        {
            var server = TestHelper.CreateTestServer(webHostBuilder => webHostBuilder.UseSupervisionApi());
            TestHelper.EnsureBlankTestDb(server);
            var client = server.CreateTestClient();
            var result = await client.GetAsync(() => TestEndpoints.PingGet);
            Assert.Equal("pong", result);
        }

        [Fact]
        public async void GithubPingTest()
        {
            var server = TestHelper.CreateTestServer(webHostBuilder => webHostBuilder.UseSupervisionApi());
            var client = TestHelper.CreateTestClient(server);
            var testWebHookPayload = new ProcessGithubEvent
            {
                Zen = "Anything added dilutes everything else.",
                HookId = 109948940,
                CreatedAt = DateTime.Now,
                Hook = new ProcessGithubEvent.HookPayload
                {
                    Id = 109948940,
                    Type = "Repository",
                    Name = "web"
                }
            };
            var expectedResult = "{\n  \"Hook\": {\n    \"Id\": 109948940,\n    \"Type\": \"Repository\",\n    \"Name\": \"web\"\n  },\n  \"Message\": \"Event accepted.\"\n}";
            var result = await client.PostJsonAsync(TestEndpoints.WebHookPost, testWebHookPayload);
            //Assert.Equal(expectedResult, result);
        }
    }
}
