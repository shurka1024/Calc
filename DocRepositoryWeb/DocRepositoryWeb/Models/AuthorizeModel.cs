using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocRepositoryWeb.Models
{
    public class AuthorizeModel
    {
        [DisplayName("Логин")]
        [Required(ErrorMessage = "Логин не указан")]
        public virtual string Login { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пароль не указан")]
        public virtual string Password { get; set; }
    }
}