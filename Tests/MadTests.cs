using System.Collections.Generic;
using System.Threading;
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

        /// <summary>
        /// По сравнению с тестом 2, теперь сначала выполняется генерация значений,
        /// а затем - чтение.
        /// </summary>
        [Test]
        public async Task Test2Async()
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

            var producerTask = producer.StartGenerateValuesAsync(service, 5);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            consumer.StartProcessValuesAsync(service, results);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            await Task.WhenAll(producerTask);

            // ASSERT
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Test3_TwinProducerAsync()
        {
            // ARRANGE

            var service = new ServiceImplementation();
            var expected = new[] {
                "foo0",
                "foobar0",
                "foo1",
                "foobar1",
                "foo2",
                "foobar2",
                "foo3",
                "foobar3",
                "foo4",
                "foobar4",
            };

            var consumer = new Consumer();
            var producer = new TwinProducer();

            var results = new List<string>();

            // ACT

            var producerTask = producer.StartGenerateValuesAsync(service, 5);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            consumer.StartProcessValuesAsync(service, results);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            await Task.WhenAll(producerTask);

            // ASSERT
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Test4_TwoProducersAsync()
        {
            // ARRANGE

            var service = new ServiceImplementation();
            var expected = new[] {
                "foo0",
                "foo0",
                "foo1",
                "foo1",
                "foo2",
                "foo2",
                "foo3",
                "foo3",
                "foo4",
                "foo4",
            };

            var consumer = new Consumer();
            var producer = new Producer();
            var producer2 = new Producer();

            var results = new List<string>();

            // ACT

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            consumer.StartProcessValuesAsync(service, results);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            var producerTask = producer.StartGenerateValuesAsync(service, 5);
            var producerTask2 = producer2.StartGenerateValuesAsync(service, 5);

            await Task.WhenAll(producerTask, producerTask2);

            // ASSERT
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Test5_5SecSufferingAsync()
        {
            // ARRANGE

            var service = new ServiceImplementation();

            var consumer = new FakeConsumer();
            var producer = new MegaTimeProducer();

            // ACT

            // Защита от бесконечного цикла.
            var cts = new CancellationTokenSource(1_000_000_000);
            var ct = cts.Token;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            consumer.StartProcessValuesAsync(service, ct);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            var producerTask = producer.StartGenerateValuesAsync(service, 5);

            await Task.WhenAll(producerTask);
            cts.Cancel();
        }

        [Test]
        public async Task Test6_TimeToWorkIsNowAsync()
        {
            // ARRANGE

            var service = new ServiceImplementation();

            var consumer = new FakeConsumer();
            var producer = new MegaTimeProducer();

            // ACT

            // Защита от бесконечного цикла.
            var cts = new CancellationTokenSource(1_000_000_000);
            var ct = cts.Token;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            consumer.StartProcessValuesAsync(service, ct);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            var producerTask = producer.StartGenerateValuesAsync(service, 60);

            await Task.WhenAll(producerTask);
            cts.Cancel();
        }
    }
}