using System;
using Microsoft.EntityFrameworkCore;
using Registration.Data;

namespace Registration.Helpers
{
    public class Seeder
    {
        private readonly DataContext dataContext;

        public Seeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Seed()
        {
            dataContext.Database.Migrate();
        }
    }
}
