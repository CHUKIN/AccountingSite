using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ManageContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemTransaction> ItemTransactions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            Database.SetInitializer(new MyContextInitializer());
        }
    }

    class MyContextInitializer : CreateDatabaseIfNotExists <ManageContext>
    {
        protected override void Seed(ManageContext db)
        {
            Role admin = new Role()
            {
                Name = "Admin"
            };
            db.Roles.Add(admin);
            Role chief = new Role()
            {
                Name = "Chief"
            };
            db.Roles.Add(chief);
            Role manager = new Role()
            {
                Name = "Manager"
            };
            db.Roles.Add(manager);
            Role engineer = new Role()
            {
                Name = "Engineer"
            };
            db.Roles.Add(engineer);



            Employee adminEmployee = new Employee()
            {
                Name = "Admin",
                 Age=10,
                  Login="admin",
                   Password="admin",
                    Role=admin
            };
            db.Employees.Add(adminEmployee);
            Employee chiefEmployee = new Employee()
            {
                Name = "chieef",
                Age = 10,
                Login = "chief",
                Password = "chief",
                Role = chief
            };
            db.Employees.Add(chiefEmployee);
            Employee managerEmployee = new Employee()
            {
                Name = "manager",
                Age = 10,
                Login = "manager",
                Password = "manager",
                Role = manager
            };
            db.Employees.Add(managerEmployee);
            Employee engineerEmployee = new Employee()
            {
                Name = "enginner",
                Age = 10,
                Login = "engineer",
                Password = "engineer",
                Role = engineer
            };
            db.Employees.Add(engineerEmployee);

            db.Statuses.Add(new Status() { Name = "Waiting for appointment" });
            db.Statuses.Add(new Status() { Name = "Assigned to" });
            db.Statuses.Add(new Status() { Name = "Sent to the warehouse" });
            db.Statuses.Add(new Status() { Name = "Lack of need" });
            db.Statuses.Add(new Status() { Name = "Marriage dispatch" });
            db.Statuses.Add(new Status() { Name = "Renouncement" });
            db.Statuses.Add(new Status() { Name = "Recycled" });
            db.Statuses.Add(new Status() { Name = "Returned" });

            db.SaveChanges();
            base.Seed(db);
        }
    }
}