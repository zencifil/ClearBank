using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IBalanceService
    {
        void DeductBalance(Account account, decimal amount);
    }
}
