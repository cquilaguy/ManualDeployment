using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class PrerrequisitoViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }
        public List<Prerequisite> Prerequisites { get; set; }
        public List<User> Users { get; set; }

        public PrerrequisitoViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Prerequisites = new List<Prerequisite>();
            Changes = new List<Change>();
            Users = new List<User>();
        }
        public void GetPrerrequisito()
        {
            if (ChangeID != 0)
            {
                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
                Prerequisites = _dbContext.Prerequisite.Where(c => c.ChangeID == ChangeID).ToList();

                // Obtener los UserIDs únicos de los contactos filtrados
                var userIds = Prerequisites.Select(c => c.UserID).Distinct().ToList();
                Users = _dbContext.User.Where(u => userIds.Contains(u.UserID)).ToList();
            }
            else
            {
                Changes = _dbContext.Change.ToList();
                Prerequisites = _dbContext.Prerequisite.ToList();
                Users = _dbContext.User.ToList();
            }
        }
    }



}
