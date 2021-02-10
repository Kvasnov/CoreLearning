using CoreLearning.DBLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public DbSet< User > Users { get; set; }
    }
}