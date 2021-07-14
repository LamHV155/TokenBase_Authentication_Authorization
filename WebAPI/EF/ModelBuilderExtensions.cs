using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultilities;
using WebAPI.EF.Entities;

namespace WebAPI.EF
{
    public static class ModelBuilderExtensions
    {

        public static  void Seed(this ModelBuilder modelBuilder)
        {
            Guid userID1 = Guid.NewGuid();
            Guid userID2 = Guid.NewGuid();
            modelBuilder.Entity<User>().HasData(
                new User { Id = userID1,
                           UserName = "viet",
                           FirstName="Viet", 
                           LastName="Lam",
                           Phone="12345",
                           Email="viet@123.com",
                           Password= MD5.Encrypt("12345") 
                },
                 new User
                 {
                     Id = userID2,
                     UserName = "tiev",
                     FirstName = "Tiev",
                     LastName = "Lam",
                     Phone = "12345",
                     Email = "viet@qwe.com",
                     Password = MD5.Encrypt("12345") 
                 }
            );

            modelBuilder.Entity<Role>().HasData(
            
                    new Role
                    {
                        Id="Admin",
                        RoleName="Admintrator"
                    },
                    new Role
                    {
                        Id="Supervisor",
                        RoleName = "Supervisor"
                    },
                    new Role
                    {
                        Id="Analyst",
                        RoleName="Analyst"
                    }
            );

            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole
                    {
                        UserId = userID1,
                        RoleId = "Admin"
                    },
                    new UserRole
                    {
                        UserId = userID2,
                        RoleId = "Supervisor"
                    }
                );

        }
    }
}
