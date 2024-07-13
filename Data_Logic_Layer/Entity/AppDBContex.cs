using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer.Entity
{
    public class AppDBContex : DbContext
    {
        public AppDBContex(DbContextOptions<AppDBContex> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
    }
}
