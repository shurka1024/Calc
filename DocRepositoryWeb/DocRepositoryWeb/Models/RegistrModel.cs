using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocRepositoryWeb.Models
{
    public class RegistrModel
    {
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        public virtual string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Укажите фамилию")]
        public virtual string LastName { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Укажите логин")]
        public virtual string Login { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Required(ErrorMessage = "Необходимо повторно ввести пароль")]
        [DisplayName("Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public virtual string Password2 { get; set; }
    }
}