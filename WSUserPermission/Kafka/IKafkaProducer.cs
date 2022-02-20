namespace WSUserPermission.Kafka
{
    public interface IKafkaProducer
    {
        public void SendKafka(string topic);
    }
}
