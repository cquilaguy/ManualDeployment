using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class FilterObj
    {
        public string ChangeNumber { get; set; }
        public int EnvironmentID { get; set; }
        public List<int> ApplicativeIDs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> UserIDs { get; set; }
        public int StatusID { get; set; }

        public FilterObj()
        {
            ApplicativeIDs = new List<int>();
            UserIDs = new List<int>();
        }
    }
}
