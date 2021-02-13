using System.ComponentModel.DataAnnotations;

namespace CoreLearning.DBLibrary.DTO_models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password {get; set;}

        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword {get; set;}

        [Required(ErrorMessage = "Не указан Name")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Не указан LastName")]
        public string LastName {get; set;}

        [Required(ErrorMessage = "Не указан Nickname")]
        public string Nickname {get; set;}
    }
}