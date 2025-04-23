using Entities;
using Microsoft.Identity.Client;

namespace Entities
{
    public class WorklogsPaged
    {

        public List<IGrouping<DateOnly,Worklog>> Worklogs { get; set; }

        public int TotalPages { get; set; }
    }
}
