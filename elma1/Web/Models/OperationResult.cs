using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Table("OperationResult")]
    public class OperationResult
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Количество переменных")]
        public int ArgumentCount { get; set; }

        [DisplayName("Переменные")]
        [MaxLength(50)]
        public string Arguments { get; set; }

        [DisplayName("Результат")]
        [MaxLength(50)]
        public string Result { get; set; }

        [DisplayName("Время выполнения")]
        public long ExecTime_ms { get; set; }

        [DisplayName("Операция")]
        public int OperationId { get; set; }

        public Operation Operation { get; set; }
    }
}