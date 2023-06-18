namespace BlazorCopilot1.Data
{
    public class Message
    {
        public DateTime TimeStamp { get; set; }
        public string? Body { get; set; }
        public bool IsRequest { get; set; }
        public Message(string body,  bool isRequest)
        {
            TimeStamp = DateTime.Now;
            Body = body;
            IsRequest = isRequest;
        }
    }
}
