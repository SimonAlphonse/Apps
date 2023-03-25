using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ChatHistory : BaseEntity
    {
        public string Topic { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
        public DateTime DateTime { get; set; }
    }
}