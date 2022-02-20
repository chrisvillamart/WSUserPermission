using Confluent.Kafka;
using Newtonsoft.Json;
using Serilog;
using WSUserPermission.Utils;

namespace WSUserPermission.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        public void SendKafka(string topic)
        {
            var config = new ProducerConfig { BootstrapServers = ConfigurationManager.AppSetting["bootstrapServers"] };
            var kafkaMessage = JsonConvert.SerializeObject(new KafkaMessage(topic));
            var p = new ProducerBuilder<Null, string>(config).Build();
            try
            {
                Log.Information($"Kafka topic: {topic}"); 
                p.Produce($"topic_wspermission_{topic}", new Message<Null, string> { Value = kafkaMessage });
                Log.Information($"Created Kafka Topic");
            }
            catch (ProduceException<Null, string> e)
            {
                Log.Information($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
