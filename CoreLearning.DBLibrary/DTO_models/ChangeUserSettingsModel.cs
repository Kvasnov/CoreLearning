using System.ComponentModel.DataAnnotations;

namespace CoreLearning.DBLibrary.DTO_models
{
    public class ChangeUserSettingsModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName {get; set;}

        [Required(ErrorMessage = "Не указан никнейм")]
        public string Nickname {get; set;}

        [Required(ErrorMessage = "Не указан Email")]
        public string Login {get; set;}

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password {get; set;}

        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword {get; set;}
    }
}