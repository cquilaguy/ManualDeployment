using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class FuncionalViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }
        public List<FunctionalUser> FunctionalUsers { get; set; }
        public List<User> Users { get; set; }

        public FuncionalViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            FunctionalUsers = new List<FunctionalUser>();
            Changes = new List<Change>();
            Users = new List<User>();
        }
        public void GetFuncional()
        {
            if (ChangeID != 0)
            {

                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
                // Obtener los blueprints asociados al ChangeID seleccionado
                FunctionalUsers = _dbContext.FunctionalUser.Where(b => b.ChangeID == ChangeID).ToList();

            }
            else
            {
                // Si no se seleccionó un ChangeID específico, obtener todos los cambios y blueprints
                Changes = _dbContext.Change.ToList();
                FunctionalUsers = _dbContext.FunctionalUser.ToList();
            }
        }
    }
}
