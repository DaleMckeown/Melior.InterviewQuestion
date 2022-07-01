using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Validators;
using System.Configuration;

namespace Melior.InterviewQuestion.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly PaymentValidator _paymentValidator;

        public PaymentService()
        {
            if (ConfigurationManager.AppSettings["DataStoreType"] == "Backup")
            {
                _accountDataStore = new BackupAccountDataStore();
            }
            else
            {
                _accountDataStore = new AccountDataStore();
            }
        }

        public PaymentService(IAccountDataStore accountDataStore, PaymentValidator paymentValidator)
        {
            _accountDataStore = accountDataStore;
            _paymentValidator = paymentValidator;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            var result = _paymentValidator.ValidPayment(account, request);

            if (result.Success)
            {
                account.Balance -= request.Amount;
                _accountDataStore.UpdateAccount(account);
            }
            return result;
        }
    }
}