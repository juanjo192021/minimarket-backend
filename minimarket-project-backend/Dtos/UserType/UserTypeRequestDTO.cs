using System.ComponentModel.DataAnnotations;

namespace minimarket_project_backend.Dtos.UserType
{
    public class UserTypeRequestDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string name { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public string? description { get; set; }

        [Required]
        public bool status { get; set; }
    }
}
