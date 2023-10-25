using FakeItEasy;
using FakeNetworkUtility.DNS;
using FakeNetworkUtility.Ping;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FakeNetworkUtility.Test.Ping
{
    public class NetworkServiceTest
    {
        private readonly NetworkService _pingService;
        private readonly IDNS _dns;

        public NetworkServiceTest()
        {
            // dependencies
            _dns = A.Fake<IDNS>();

            // SUT(System Under Test)
            _pingService = new NetworkService(_dns);
        }
        // Facts are tests which are always true. They test invariant conditions.
        [Fact]
        public void NetworkService_SendPing_ReturnsString()
        {
            //arrange
            A.CallTo(() => _dns.SendDns()).Returns(true);
            // act
            var result = _pingService.SendPing();

            // assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().NotBeEmpty();
            result.Should().Be("Success: Ping Sent");
            result.Should().Contain("Success", Exactly.Once());
        }

        // Theories are tests which are only true for a particular set of data.
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeout_ReturnSumOfNums(int a, int b, int expected)
        {
            // act
            var result = _pingService.PingTimeout(a, b);

            // assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnsDate()
        {
            var result = _pingService.LastPingDate();

            // last ping date should be 5 seconds before now
            result.Should().BeAfter(DateTime.Now.Add(new TimeSpan(0, 0, -5)));
            // last ping date should be 5 seconds after now
            result.Should().BeBefore(DateTime.Now.Add(new TimeSpan(0, 0, 5)));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject()
        {
            var expected = new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            };

            var result = _pingService.GetPingOptions();

            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnsIEnumarable()
        {
            var expected = new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            };

            var result = _pingService.MostRecentPings();

            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
            foreach (var item in result)
            {
                item.Ttl.Should().BeGreaterThanOrEqualTo(0);
                item.Ttl.Should().BeLessThanOrEqualTo(100);
            }
            for(int i = 0; i < result.Count(); i++)
            {
                if (i % 2 == 0) result.ElementAt(i).DontFragment.Should().Be(true);
                else result.ElementAt(i).DontFragment.Should().Be(false);
            }
        }

    }
}
