using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class ChangeViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }

        public ChangeViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Changes = new List<Change>();
        }

        public void GetChange()
        {
            if (ChangeID != 0)
            {
                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
            }
            else
            {
                Changes = _dbContext.Change.ToList();
            }
        }
    }
}
