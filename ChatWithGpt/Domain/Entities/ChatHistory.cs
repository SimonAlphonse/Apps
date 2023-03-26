using System.Net;

namespace Domain.Entities
{
    public class ChatHistory : BaseEntity
    {
        public string Title { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public DateTime DateTime { get; set; }
    }
}