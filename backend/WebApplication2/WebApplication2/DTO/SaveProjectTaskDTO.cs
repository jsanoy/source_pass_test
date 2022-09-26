using WebApplication2.DbEntities;

namespace WebApplication2.DTO
{
	public class SaveProjectTaskDTO
	{
		public int ProjectId { get; set; }
		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
		public double? TotalEstimate { get; set; }
		public List<SaveTaskEmployeeDTO> Employees { get; set; }

	}

	public partial class SaveTaskEmployeeDTO
	{
		public int EmployeeId { get; set; }
		public decimal? EstimatedHours { get; set; }
		public decimal? ActualHours { get; set; }
	}

}
