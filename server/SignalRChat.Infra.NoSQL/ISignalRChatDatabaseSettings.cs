namespace SignalRChat.Infra.NoSQL
{
    public class SignalRChatDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string MessagesCollectionName { get; set; }
    }
}