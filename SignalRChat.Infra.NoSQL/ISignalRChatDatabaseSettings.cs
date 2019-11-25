namespace SignalRChat.Infra.NoSQL
{
    public class SignalRChatDatabaseSettings : ISignalRChatDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string MessagesCollectionName { get; set; }
    }

    public interface ISignalRChatDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string MessagesCollectionName { get; set; }
    }
}