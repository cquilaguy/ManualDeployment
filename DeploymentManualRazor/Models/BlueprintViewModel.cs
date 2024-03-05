using Repository.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DeploymentManualRazor.Models
{
    public class BlueprintViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Blueprint> Blueprints { get; set; }
        public int ChangeID { get; set; }
        public List<Change> Changes { get; set; }

        public BlueprintViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Blueprints = new List<Blueprint>();
            Changes = new List<Change>();
        }

        public void GetBlueprints()
        {
            if (ChangeID != 0)
            {
               

                    // Obtener los blueprints asociados al ChangeID seleccionado
                    //Blueprints = _dbContext.Blueprint.Where(b => b.ChangeID == ChangeID).ToList();
              
            }
            else
            {
                // Si no se seleccionó un ChangeID específico, obtener todos los cambios y blueprints
             
                Blueprints = _dbContext.Blueprint.ToList();
            }
        }


    }
}
