using System;
namespace kwetter_backend.Models
{
    public class Kweet
    {
            public Guid Id { get; set; }
            public Guid ProfileId { get; set; }
            public string Message { get; set; }
            public DateTime DateOfCreation { get; set; }
    }
}
