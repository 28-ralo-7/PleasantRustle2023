using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PleasantRustle.Dto
{
    public class AgentDto
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string logoPath { get; set; }
        public Nullable<int> priority { get; set; }
        public string typeAgent { get; set; }
        public int discount { get; set; }
        public Nullable<int> saleOfYearCount { get; set;}
        public string email { get; set; }
    }
}
