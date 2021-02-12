using System;

namespace CoreLearning.DBLibrary.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string login, Guid userId);
    }
}