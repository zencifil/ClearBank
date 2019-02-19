using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IAccountDataStore _accountDataStore;

        public BalanceService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore;
        }

        public void DeductBalance(Account account, decimal amount)
        {
            account.Balance -= amount;
            _accountDataStore.UpdateAccount(account);
        }
    }
}
