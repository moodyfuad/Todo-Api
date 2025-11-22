using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistant.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistant
{
    public sealed class RepositoryDbContext : DbContext
        //<
        //AppUser,
        //IdentityRole<Guid>,
        //Guid,
        //IdentityUserClaim<Guid>,
        //IdentityUserRole<Guid>,
        //IdentityUserLogin<Guid>,
        //IdentityRoleClaim<Guid>,
        //IdentityUserToken<Guid>>
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<PersonTaskItem> PersonTaskItems { get; set; }
        public DbSet<TaskItemGroup> TaskItemGroups { get; set; }
        public DbSet<TaskItemNote> TaskItemNotes { get; set; }
        public DbSet<PersonTaskItemGroup> PersonTaskItemGroup{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);

            modelBuilder.Entity<BaseEntity>().UseTpcMappingStrategy();

            base.OnModelCreating(modelBuilder);
           
        }
    }
}
