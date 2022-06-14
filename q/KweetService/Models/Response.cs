using System;
namespace kwetter_backend.Models
{
    public class Response<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = false;
        
    }
}
