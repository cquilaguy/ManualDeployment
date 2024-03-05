using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class changeInformationViewModel
    {
        public string RequesType { get; set; }
        public string ChangeNumber { get; set; }
        public string ChangeType { get; set; }
        public string ChangeDescrip { get; set; }
        public string ChangeOwner { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string changeDeveloper { get; set; }
    }
}
