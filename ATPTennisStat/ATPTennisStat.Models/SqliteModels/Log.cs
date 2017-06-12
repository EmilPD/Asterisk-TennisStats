using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATPTennisStat.Models.SqliteModels
{
    public class Log
    {
        private ICollection<LogDetail> logDetails;

        public Log()
        {
            this.logDetails = new HashSet<LogDetail>();
        }

        public int Id { get; set; }

        [StringLength(5000)]
        public string Message { get; set; }

        public DateTime? TimeStamp { get; set; }

        public virtual ICollection<LogDetail> LogDetails
        {
            get { return this.logDetails; }
            set { this.logDetails = value; }
        }
    }
}