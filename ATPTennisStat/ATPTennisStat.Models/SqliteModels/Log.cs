using System;
using System.ComponentModel.DataAnnotations;

namespace ATPTennisStat.Models.SqliteModels
{
    public class Log
    {
        public int Id { get; set; }

        [StringLength(5000)]
        public string Message { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}