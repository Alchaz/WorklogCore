using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int Id { get; set; } 

        public string Name { get; set; }    

        public int DailyMinHours { get; set; }

        public int DailyMaxHours { get; set; }

        public string Role { get; set; }

        [JsonIgnore]
        public virtual List<Worklog> Worklogs { get; set; }

        public User()
        {
            Worklogs = new List<Worklog>();
            Name = String.Empty;
        }

    }
}
