using FICR.Cooperation.Humanism.Services.Base;

namespace FICR.Cooperation.Humanism.Services.Facebook
{
    public class FacebookApiService : ApiServiceBase
    {
        public FacebookApiService(HttpClient httpClient)
            : base(httpClient, "https://graph.facebook.com/v20.0/422178017639237/") // Endpoint base no construtor base
        {
        }

        public async Task SendMenssageAsync(string recipientPhoneNumber, string templateName)
        {
            var parameters = new
            {
                messaging_product = "whatsapp",
                to = recipientPhoneNumber,
                type = "template",
                template = new
                {
                    name = templateName,
                    language = new { code = "en_US" }
                }
            };

            await CallApiAsync("messages", parameters);
        }
    }
}
