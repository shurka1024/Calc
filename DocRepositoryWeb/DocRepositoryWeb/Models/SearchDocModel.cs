using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocRepositoryWeb.Models
{
    public class SearchDocModel
    {
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Укажите наименование")]
        public virtual string Name { get; set; }
    }
}