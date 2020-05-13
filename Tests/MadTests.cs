using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Lib;

using NUnit.Framework;

namespace Tests
{
    class MadTests
    { 
        [Test]
        public async Task Test1Async()
        {
            // ARRANGE

            var service = new ServiceImplementation();
            var expected = new[] {
                "foo0",
                "foo1",
                "foo2",
                "foo3",
                "foo4",
            };

            var consumer = new Consumer();
            var producer = new Producer();

            var results = new List<string>();

            // ACT

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            consumer.StartProcessValuesAsync(service, results);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            var producerTask = producer.StartGenerateValuesAsync(service, 5);

            await Task.WhenAll(producerTask);

            // ASSERT
            results.Should().BeEquivalentTo(expected);
        }
    }
}