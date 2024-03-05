using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class FilterDTO
    {
        public string ChangeNumber { get; set; }
        public int EnvironmentID { get; set; }
        public int ApplicativeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
    }
}
