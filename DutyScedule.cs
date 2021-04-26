using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
   public class DutyScedule                    //for setting their duty
    {
        public int Id { get; set; }
        public DateTime DutyDate { get; set; }
        public int UserId { get; set; }
    }
}
