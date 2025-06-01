using Confluent.Kafka;
using System.Text;

namespace PaymentService.Helper;

public static class Producer
{
	const string HOST = "localhost:9092";
	const string TOPIC = "payment-service-add-booking";

	public static async void ProduceMessage(string message, string messageType)
	{
		var producerConfig = new ProducerConfig(
			new Dictionary<string, string>{
				{"bootstrap.servers", HOST},
				{"security.protocol", "PLAINTEXT"},
			});

		using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
		try
		{
			var deliveryResult = await producer.ProduceAsync(
				TOPIC,
				new Message<Null, string>
				{
					Value = message,
					Headers = new()
					{
						{ "Type", Encoding.UTF8.GetBytes(messageType) }
					}
				});
			Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
		}
		catch (ProduceException<Null, string> e)
		{
			Console.WriteLine($"Delivery failed: {e.Error.Reason}");
		}
	}
}
