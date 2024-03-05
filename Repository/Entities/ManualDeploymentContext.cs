using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Entities
{
    public class ManualDeploymentContext : DbContext
    {
        public ManualDeploymentContext(DbContextOptions<ManualDeploymentContext> options)
            : base(options)
        {
        }
        public DbSet<Applicative> Applicative { get; set; }
        //public DbSet<EnvironmentApplicative> EnvironmentApplicatives { get; set; }
        public DbSet<Blueprint> Blueprint { get; set; }
        public DbSet<Change> Change{ get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Environment> Environment { get; set; }
        public DbSet<FunctionalUser> FunctionalUser { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Postimplantation> Postimplantation { get; set; }
        public DbSet<Prerequisite> Prerequisite{ get; set; }
        public DbSet<Result> Result { get; set; }
        //public DbSet<Rol> Rols { get; set; }
        //public DbSet<ProfileUser> RolUsers { get; set; }
        public DbSet<Server> Server { get; set; }
        public DbSet<Signature>Signature  { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ChangeApplicative> ChangeApplicative { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileUser> ProfileUser { get; set; }
        public DbSet<ResponsibleArea> ResponsibleArea { get; set; }
        public DbSet<RollbackPlan> RollbackPlan { get; set; }
        public DbSet<RollbackPre> RollbackPre { get; set; }
        public DbSet<RequestType> RequestType { get; set; }
        public DbSet<Typology> Typology { get; set; }
        public DbSet<Type> Type { get; set; }
        public DbSet<Status> Status { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }





    }
}
