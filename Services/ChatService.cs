using Azure;

using Azure.AI.OpenAI;
using BlazorCopilot1.Data;

namespace BlazorCopilot1.Services
{
    public class ChatService
    {
        private readonly IConfiguration _configuration;

        private string SystemMessage = "You are an AI assistant that helps people find information about food.  For anything other than food, respond with 'I can only answer questions about food.'";

        public ChatService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Message> GetResponse(List<Message> messagechain)
        {
            string response = "";

            OpenAIClient client = new OpenAIClient(
                new Uri(_configuration.GetSection("Azure")["OpenAIUrl"]!),
                new AzureKeyCredential(_configuration.GetSection("Azure")["OpenAIKey"]!));

            
            ChatCompletionsOptions options = new ChatCompletionsOptions();
            options.Temperature = (float)0.7;
            options.MaxTokens = 800;
            options.NucleusSamplingFactor = (float)0.95;
            options.FrequencyPenalty = 0;
            options.PresencePenalty = 0;
            options.Messages.Add(new ChatMessage(ChatRole.System,SystemMessage));
            foreach (var msg in messagechain)
            {
                if (msg.IsRequest)
                {
                    options.Messages.Add(new ChatMessage(ChatRole.User, msg.Body));
                }
                else
                {
                    options.Messages.Add(new ChatMessage(ChatRole.Assistant, msg.Body));
                }
            }

            Response<ChatCompletions> resp = await client.GetChatCompletionsAsync(
                _configuration.GetSection("Azure")["OpenAIDeploymentModel"]!,
                options);

            ChatCompletions completions = resp.Value;

            response = completions.Choices[0].Message.Content;

            Message responseMessage = new Message(response,false);
            return responseMessage;
        }
        
    }
}
