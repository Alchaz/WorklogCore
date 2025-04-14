using Data.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class WorklogRepository : Repository<Worklog>, IWorklogRepository
    {
        private readonly WorklogContext _context;

        public WorklogRepository(WorklogContext context) : base(context)
        {
            _context = context;
        }
    }
}
