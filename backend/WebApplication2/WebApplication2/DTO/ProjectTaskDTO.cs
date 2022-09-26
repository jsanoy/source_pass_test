using WebApplication2.DbEntities;

namespace WebApplication2.DTO
{
	public class ProjectTaskDTO
	{
		public int TaskId { get; set; }
		public int ProjectId { get; set; }
		public string ProjectName { get; set; }

		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
		public double? TotalEstimate { get; set; }
		public List<TaskEmployeeDTO> Employees { get; set; }

	}

	public partial class TaskEmployeeDTO
	{
		public int id { get; set; }
		public int EmployeeId { get; set; }
		public string EmployeeName { get; set; }

		public decimal? EstimatedHours { get; set; }
		public decimal? ActualHours { get; set; }
	}

}
