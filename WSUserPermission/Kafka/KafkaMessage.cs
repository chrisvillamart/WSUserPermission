using System;
namespace WSUserPermission.Kafka
{
    public class KafkaMessage
    { 
        public KafkaMessage(string name)
        {
            this.Name = name;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}
