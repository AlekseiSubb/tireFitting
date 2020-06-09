using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tireFitting.Models;

namespace tireFitting
{
    public class InitData
    {
        public static void Initialize(TireFittingContext context)
        { 

            if (!context.Peoples.Any())
            {
                context.Peoples.Add(
                new People
                {
                    Name = "Администратор",
                    Phone = "123",
                    Password = "admin"
                });             
                context.SaveChanges();
            }

        }
    }
}
