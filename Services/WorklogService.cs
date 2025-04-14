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

        public async Task<List<IGrouping<DateOnly, Worklog>>> GetWorklogsGroupedByDay(string token)
        {
            var claims = Authentication.GetClaimsFromToken(token);
            string role = claims.Where(c => c.Type.Equals(JwtRegisteredClaimNames.Typ)).FirstOrDefault().Value;
            string userName = claims.Where(c=>c.Type.Equals(JwtRegisteredClaimNames.Sub)).FirstOrDefault().Value;
            List<IGrouping<DateOnly, Worklog>> groupedWorklogs;
            if (role.Equals(Helpers.UserRoleEnum.Admin.ToString()))
            {
                 groupedWorklogs = await _worklogRepository.GetAllQueryable()
                                          .Include(c=>c.User)
                                          .GroupBy(w => w.date)
                                          .ToListAsync();
            }
            else
            {
                groupedWorklogs = await _worklogRepository.GetAllQueryable().Where(c => c.User.Name.Equals(userName))
                                             .Include(c => c.User)
                                            .GroupBy(w => w.date)
                                            .ToListAsync();
            }

            return groupedWorklogs;
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
