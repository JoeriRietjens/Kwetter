using System.ComponentModel.DataAnnotations;

namespace kwetter_backend.models
{
    public class CreateKweetRequest
    {
        [Required]
        [StringLength(100)]
        public string Message { get; set; }
    }
}