using Microsoft.AspNetCore.Mvc;
using WebApplication2.DbEntities;
using WebApplication2.DTO;

namespace WebApplication2.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProjectTaskController : Controller
	{
		private ProjectService _project;

		public ProjectTaskController(ProjectService project)
		{
			_project = project;
		}

		[HttpPost("savetask")]
		public object SaveTask(SaveProjectTaskDTO dto)
		{
			int i = _project.SaveProjectTask(dto);
			return new {
				taskId = i
			};
		}

		[HttpGet("getTask")]
		public ProjectTaskDTO getTask(int id)
		{
			return _project.getProjectTask(id);
		}

		[HttpGet("getprojects")]
		public List<Project> getProjects()
		{
			return _project.getProjects();
		}

		[HttpGet("getemployees")]
		public List<Employee> getemployees()
		{
			return _project.getEmployees();
		}
	}
}
