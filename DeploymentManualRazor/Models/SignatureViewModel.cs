using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualRazor.Models
{
    public class SignatureViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }
        public List<Signature> Signatures { get; set; }
        public List<User> Users { get; set; }

        public SignatureViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Signatures = new List<Signature>();
            Changes = new List<Change>();
            Users = new List<User>();
        }

        public void GetSignature()


        {
            if (ChangeID != 0)
            {
                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
                Signatures = _dbContext.Signature.Where(c => c.ChangeID == ChangeID).ToList();

                // Obtener los UserIDs únicos de los contactos filtrados
                var userIds = Signatures.Select(c => c.UserID).Distinct().ToList();
                Users = _dbContext.User.Where(u => userIds.Contains(u.UserID)).ToList();
            }
            else
            {
                Changes = _dbContext.Change.ToList();
                Signatures = _dbContext.Signature.ToList();
                Users = _dbContext.User.ToList();
            }   
        }
    }
}
