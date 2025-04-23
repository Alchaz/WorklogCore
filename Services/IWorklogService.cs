using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWorklogService
    {


        Task CreateWorklog(Worklog worklog, string token);


        Task<WorklogsPaged> GetWorklogsGroupedByDay(string token, string filter ,  int page ,  int pageSize);

        Task<List<Worklog>> GetWorklogs(string token);

        Task UpdateWorkedHours(int idWorklog, decimal workedHours, string token);

    }
}
