using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication2.DbEntities
{
    public partial class TaskEmployee
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public decimal? ActualHours { get; set; }
    }
}
