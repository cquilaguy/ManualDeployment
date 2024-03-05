using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class PosinViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }
        public List<Postimplantation> Postimplantations { get; set; }
        public List<User> Users { get; set; }

        public PosinViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Postimplantations = new List<Postimplantation>();
            Changes = new List<Change>();
            Users = new List<User>();
        }
        public void GetPosin()
        {
            if (ChangeID != 0)
            {

                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
                // Obtener los blueprints asociados al ChangeID seleccionado
                Postimplantations = _dbContext.Postimplantation.Where(b => b.ChangeID == ChangeID).ToList();

            }
            else
            {
                // Si no se seleccionó un ChangeID específico, obtener todos los cambios y blueprints
                Changes = _dbContext.Change.ToList();
                Postimplantations = _dbContext.Postimplantation.ToList();
            }
        }
    }
}
