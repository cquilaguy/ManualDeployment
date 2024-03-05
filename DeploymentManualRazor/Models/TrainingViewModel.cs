using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class TrainingViewModel
    {

        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }
        public List<Training> Trainings { get; set; }
        public List<User> Users { get; set; }

        public TrainingViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Trainings = new List<Training>();
            Changes = new List<Change>();
            Users = new List<User>();
        }

        public void GetTraining()
        {
            if (ChangeID != 0)
            {
                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
                Trainings = _dbContext.Training.Where(c => c.ChangeID == ChangeID).ToList();

                // Obtener los UserIDs únicos de los contactos filtrados
                var userIds = Trainings.Select(c => c.UserID).Distinct().ToList();
                Users = _dbContext.User.Where(u => userIds.Contains(u.UserID)).ToList();
            }
            else
            {
                Changes = _dbContext.Change.ToList();
                Trainings = _dbContext.Training.ToList();
                Users = _dbContext.User.ToList();
            }
        }

    }
}
