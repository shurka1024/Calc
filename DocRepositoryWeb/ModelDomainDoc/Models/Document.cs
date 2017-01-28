using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomainDoc.Models
{
    [Table("Documents")]
    public class Document
    {
        [Key]
        public virtual int Id { get; set; }
        [DisplayName("Наименование")]
        public virtual string Name { get; set; }
        [DisplayName("Дата создания")]
        public virtual DateTime Date { get; set; }
        [DisplayName("Автор")]
        public virtual int AutorId { get; set; }
        public virtual string BinaryFile { get; set; }
        public virtual User Autor { get; set; }
    }
}
