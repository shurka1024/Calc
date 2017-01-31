using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDomainDoc.Models
{
    [Table("Documents")]
    public class Document
    {
        [Key]
        public virtual int Id { get; set; }

        
        [Required]
        public virtual string ShotName
        {
            get {return (Name.Length > 31) ? $"{Name.Substring(0,30)}..." : Name; }
        }
        [DisplayName("Наименование")]
        [Required]
        public virtual string Name { get; set; }
        
        [DisplayName("Дата создания")]
        [Required]
        public virtual DateTime Date { get; set; }

        [DisplayName("Автор")]
        [Required]
        public virtual int AutorId { get; set; }

        [DisplayName("Выберите файл")]
        [Required]
        public virtual string BinaryFile { get; set; }

        public virtual User Autor { get; set; }
    }
}
