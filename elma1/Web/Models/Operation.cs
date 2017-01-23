using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Table("Operation")]
    public class Operation
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }
    }
}