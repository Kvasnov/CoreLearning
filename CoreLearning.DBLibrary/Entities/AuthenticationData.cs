using System;

namespace CoreLearning.DBLibrary.Entities
{
    public class AuthenticationData
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
    }
}