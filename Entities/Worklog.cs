using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Worklog
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateOnly date { get; set; }
        public decimal WorkedHours { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }


        public Worklog() 
        {
            Description = string.Empty;
        }

    }
}
