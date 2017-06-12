using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Models.SqliteModels
{
    public class LogDetail
    {
        public int Id { get; set; }

        [StringLength(5000)]
        public string Message { get; set; }

        public DateTime? TimeStamp { get; set; }

        [ForeignKey("Log")]
        public int? LogId { get; set; }

        public Log Log { get; set; }
    }
}
