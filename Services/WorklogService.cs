using Data;
using Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorklogService : IWorklogService
    {
        private readonly IWorklogRepository _worklogRepository;
        public WorklogService(IWorklogRepository worklogRepository) 
        {
            _worklogRepository = worklogRepository;
        }


        public async Task CreateWorklog(Worklog worklog, string token)
        {
            var claims = Authentication.GetClaimsFromToken(token);
            string userId = claims.Where(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)).FirstOrDefault().Value;
            worklog.UserId = Convert.ToInt32(userId);
            await _worklogRepository.AddAsync(worklog);
        }

        public async Task<WorklogsPaged> GetWorklogsGroupedByDay(string token, string filter, int page, int pageSize)
        {
            var claims = Authentication.GetClaimsFromToken(token);
            string role = claims.Where(c => c.Type.Equals(JwtRegisteredClaimNames.Typ)).FirstOrDefault().Value;
            string userName = claims.Where(c=>c.Type.Equals(JwtRegisteredClaimNames.Sub)).FirstOrDefault().Value;
            IQueryable<Worklog> query = _worklogRepository.GetAllQueryable()
            .Include(w => w.User);

            if (!role.Equals(Helpers.UserRoleEnum.Admin.ToString()))
            {
                query = query.Where(w => w.User.Name.Equals(userName));
            }

            if (filter.Equals("OT"))
            {
                query = query.Where(w => w.User.DailyMaxHours < w.WorkedHours);
            }
            else if (filter.Equals("UT"))
            {
                query = query.Where(w => w.User.DailyMinHours > w.WorkedHours);
            }
            WorklogsPaged worklogsPaged = new WorklogsPaged();
            List<DateOnly> totalDates = await query.Select(c => c.date).OrderByDescending(x=>x).Distinct().ToListAsync();
            List<DateOnly> datesOfPage = totalDates
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList(); 
            worklogsPaged.TotalPages = (int)Math.Ceiling(totalDates.Count() / (double)pageSize); 
            worklogsPaged.Worklogs = (await query.Where(c => datesOfPage.Contains(c.date)).ToListAsync()).GroupBy(c => c.date).ToList();
            return worklogsPaged;
        }

        public async Task<List<Worklog>> GetWorklogs(string token)
        {
            var claims = Authentication.GetClaimsFromToken(token);
            string userName = claims.Where(c => c.Type.Equals(JwtRegisteredClaimNames.Sub)).FirstOrDefault().Value;
            List<Worklog> worklogs = new List<Worklog>();
            worklogs = await _worklogRepository.GetAllQueryable().Where(c => c.User.Name.Equals(userName)).Include(c => c.User).ToListAsync();
            return worklogs;
        }

        public async Task UpdateWorkedHours(int idWorklog, decimal workedHours, string token)
        {
            var claims = Authentication.GetClaimsFromToken(token);
            int userId = Convert.ToInt32(claims.Where(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)).FirstOrDefault().Value);
            Worklog worklog =await  _worklogRepository.GetByIdAsync(idWorklog);
            if(worklog.UserId == userId)
            {
                worklog.WorkedHours = workedHours;
                await _worklogRepository.UpdateAsync(worklog);
            }
            else
            {
                throw new UnauthorizedAccessException("You are not authorized to update this worklog.");
            }
        }

    }
}
