using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Redis101.Services;
using StackExchange.Redis;
using Xunit;

namespace Redis101_Tests
{
    public class CacheServiceTests
    {
        private string _redisKey = "test";
        private string _redisValue = "value";

        [Fact]
        public async Task ShouldGetARegisterFromRedis()
        {
            var mocker = new AutoMocker();

            var databaseInstanceStub = mocker.GetMock<IDatabase>();

            mocker.Setup<IConnectionMultiplexer, IDatabase>(x => 
                x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseInstanceStub.Object)
                .Verifiable();

            mocker.Setup<IDatabase, Task<RedisValue>>(
                x => x.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(_redisValue)
                .Verifiable();

            var sut = mocker.CreateInstance<CacheService>();

            var response = await sut.GetCacheValueAsync(_redisKey);

            response.Should().NotBeNull();
            response.Should().Be(_redisValue);

            mocker.Verify<IDatabase>(x => 
                x.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()), Times.Once);

            mocker.VerifyAll();
        }

        [Fact]
        public async Task ShouldSetARegisterToRedis()
        {
            var mocker = new AutoMocker();

            var databaseInstanceStub = mocker.GetMock<IDatabase>();

            mocker.Setup<IConnectionMultiplexer, IDatabase>(x => 
                x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseInstanceStub.Object)
                .Verifiable();

            mocker.Setup<IDatabase, Task<bool>>(
                x => x.StringSetAsync(
                    It.IsAny<RedisKey>(), 
                    It.IsAny<RedisValue>(), 
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<When>(),
                    It.IsAny<CommandFlags>()))
                .ReturnsAsync(true)
                .Verifiable();

            var sut = mocker.CreateInstance<CacheService>();

            await sut.SetCacheValueAsync(_redisKey, _redisValue);

            mocker.Verify<IDatabase>(x => 
                x.StringSetAsync(
                    It.IsAny<RedisKey>(), 
                    It.IsAny<RedisValue>(), 
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<When>(),
                    It.IsAny<CommandFlags>()), Times.Once);
            
            mocker.VerifyAll();
        }
    }
}
