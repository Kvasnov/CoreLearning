using System.Collections.Generic;
using System.Linq;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.Interfaces;

namespace CoreLearning.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        public UserRepository( UserContext context )
        {
            this.context = context;
        }

        private readonly UserContext context;

        public List< User > GetUsers()
        {
            return context.Users.ToList();
        }

        public void AddUser( User user )
        {
            context.Users.Add( user );
        }
    }
}