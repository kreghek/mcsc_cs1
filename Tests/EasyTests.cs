using System.Threading.Tasks;
using FluentAssertions;

using Lib;

using NUnit.Framework;

namespace Tests
{
    public class EasyTests
    {
        [Test]
        public async Task Test1Async()
        {
            // ARRANGE

            var service = new ServiceImplementation();

            // ACT

            var task = service.GetValueAsync("foo");
            service.SetValue("bar");
            var factValue = await task;

            // ASSERT
            factValue.Should().Be("foobar");
        }

        [Test]
        public async Task Test2Async()
        {
            // ARRANGE

            var service = new ServiceImplementation();

            // ACT

            service.SetValue("bar");
            var task = service.GetValueAsync("foo");
            var factValue = await task;

            // ASSERT
            factValue.Should().Be("foobar");
        }

        [Test]
        public async Task Test3Async()
        {
            // ARRANGE

            var service = new ServiceImplementation();

            // ACT

            service.SetValue("bar");
            var task1 = service.GetValueAsync("foo");
            var factValue1 = await task1;

            service.SetValue("baz");
            var task2 = service.GetValueAsync("foo");
            var factValue2 = await task2;

            // ASSERT
            factValue1.Should().Be("foobar");
            factValue2.Should().Be("foobaz");
        }

    }
}