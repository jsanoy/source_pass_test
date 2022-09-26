using WebApplication2.DTO;
using Dapper;
using System.Data.SqlClient;
using WebApplication2.DbEntities;

namespace WebApplication2
{
	public class ProjectService
	{
		public string db = "server=(local);Integrated security = true;database=testdb;";

		public List<Project> getProjects()
		{
			using(var con = new SqlConnection(db)) {
				return con.Query<Project>("select * from projects").ToList();
			}
		}
		public List<Employee> getEmployees()
		{
			using (var con = new SqlConnection(db)) {
				return con.Query<Employee>("select * from employee").ToList();
			}
		}
			public int SaveProjectTask(SaveProjectTaskDTO dto)
		{
			using(var con = new SqlConnection(db)) {

				con.Open();
			
				using(var dbtrx = con.BeginTransaction()) {

					var p = new {
						projectId = dto.ProjectId,
						taskTitle = dto.TaskTitle,
						taskDescription = dto.TaskDescription,
						totalEstimated = dto.TotalEstimate,
						employees = string.Join(";", dto.Employees.Select(o => string.Join("|", o.EmployeeId, o.EstimatedHours, o.ActualHours)))
					};
					con.Execute("exec save_project_task @projectId, @taskTitle, @taskDescription, @totalEstimated, @employees ",p,dbtrx);

					var task_id = con.QueryFirst<int>("select max(id) from project_task", null, dbtrx);

					dbtrx.Commit();

					return task_id;
				}
				
			}
		}

		public ProjectTaskDTO getProjectTask(int id)
		{
			using (var con = new SqlConnection(db)) {

				var t = con.QueryFirst<ProjectTaskDTO>(@"
						select pt.id as taskId,
							projectid, p.ProjectName,TaskTitle,TaskDescription, TotalEstimate
						from project_task pt inner join projects p on pt.projectid = p.id 
						where pt.id = @id ", new { id = id });
				if (t == null)
					return null;

				t.Employees = con.Query<TaskEmployeeDTO>(@"
						select  te.id, te.EmployeeId, e.EmployeeName, EstimatedHours, ActualHours
						from task_employees te inner join employee e on te.employeeid = e.id 
						where taskid = @taskid ", new { taskid = t.TaskId }).ToList();


				return t;

			}
		}
	}
}
