﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("UPDATE_LOG")]
    public class UpdateLog : BaseEntity
    {
        [Column("TRANSACTION_DATE")]
        public DateTime TransactionDate { get; set; }
        [Column("OFFSET")]
        public int OffSet { get; set; }
        [Column("SUCCESS")]
        public bool Success { get; set; }
        [Column("MESSAGE")]
        public string Message { get; set; }
        [Column("ENTITY_COUNT")]
        public int EntityCount { get; set; }
    }
}