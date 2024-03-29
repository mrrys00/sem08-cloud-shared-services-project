using Microsoft.Extensions.DependencyInjection;
using KafkaFlow.Producers;
using KafkaFlow.Serializer;
using KafkaFlow;

namespace NetBank;

internal class Telemetry
{
    private readonly IMessageProducer _producer;

    public Telemetry(int port)
    {
        var brokerUrl = $"{Config.Localhost}:{port}";
        var services = new ServiceCollection();

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(new[] { brokerUrl })
                        .CreateTopicIfNotExists(Config.TopicName, 1, 1)
                        .AddProducer(
                            Config.ProducerName,
                            producer => producer
                                .DefaultTopic(Config.TopicName)
                                .AddMiddlewares(m => m
                                    .AddSerializer<JsonCoreSerializer>()))));
        _producer = services
            .BuildServiceProvider()
            .GetRequiredService<IProducerAccessor>()
            .GetProducer(Config.ProducerName);

        Helper.WriteLineColored(
            $"Starting telemetry on: http://{brokerUrl}", ConsoleColor.Blue);
    }

    public async Task Produce(string key, object json)
    {
        await _producer.ProduceAsync(Config.TopicName, key, json);

        Helper.WriteLineColored(
            "Log sent!", ConsoleColor.Blue);
    }
}
