using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class UserViewModel
    {

        private readonly ManualDeploymentContext _dbContext;

        public List<User> User { get; set; }

        public UserViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            User = new List<User>();
        }

        public void GetUsers()
        {
            User = _dbContext.User.ToList();
        }
    }
}
