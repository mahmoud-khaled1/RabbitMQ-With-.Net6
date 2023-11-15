namespace FormulaAirline.API.Services
{
    public interface IMessagesProducer
    {
        public void SendMessage<T>(T message);
    }
}
