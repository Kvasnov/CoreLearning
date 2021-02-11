using System.Threading.Tasks;

namespace CoreLearning.DBLibrary.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(string login);
    }
}