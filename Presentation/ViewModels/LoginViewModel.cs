using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public int AuthToken { get; set; }
    }
}
