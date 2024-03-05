using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeploymentManualRazor.Models
{
    public class ContactViewModel
    {
        private readonly ManualDeploymentContext _dbContext;

        public List<Change> Changes { get; set; }
        public int ChangeID { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<User> Users { get; set; }


        public ContactViewModel(ManualDeploymentContext dbContext)
        {
            _dbContext = dbContext;
            Contacts = new List<Contact>();
            Changes = new List<Change>();
            Users = new List<User>();
        }
        public void GetContact()


        {
            if (ChangeID != 0)
            {
                Changes = _dbContext.Change.Where(c => c.ChangeID == ChangeID).ToList();
                Contacts = _dbContext.Contact.Where(c => c.ChangeID == ChangeID).ToList();

                // Obtener los UserIDs únicos de los contactos filtrados
                var userIds = Contacts.Select(c => c.UserID).Distinct().ToList();
                Users = _dbContext.User.Where(u => userIds.Contains(u.UserID)).ToList();
            }
            else
            {
                Changes = _dbContext.Change.ToList();
                Contacts = _dbContext.Contact.ToList();
                Users = _dbContext.User.ToList();
            }
        }

    }

   
    
}
