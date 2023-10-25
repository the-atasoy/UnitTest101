using FakeNetworkUtility.Ping;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNetworkUtility.Test.Ping
{
    public class NetworkServiceTest
    {
        [Fact]
        public void NetworkService_SendPing_ReturnsString()
        {
            // arrange
            var pingService = new NetworkService();

            // act
            var result = pingService.SendPing();

            // assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().NotBeEmpty();
            result.Should().Be("Success: Ping Sent");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeout_ReturnSumOfNums(int a, int b, int expected)
        {
            // arrange
            var pingService = new NetworkService();

            // act
            var result = pingService.PingTimeout(a, b);

            // assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(0);
        }

    }
}
