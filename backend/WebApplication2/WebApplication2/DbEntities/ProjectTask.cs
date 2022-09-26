using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication2.DbEntities
{
    public partial class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public double? TotalEstimate { get; set; }
    }
}
