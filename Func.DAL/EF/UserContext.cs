namespace Func.DAL.EF
{
    using Func.DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("ConnectionUser")
        {
        }

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Chart> Charts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().HasMany(o => o.Charts)
                .WithOptional(o => o.UserData);

            base.OnModelCreating(modelBuilder);
        }
    }
}