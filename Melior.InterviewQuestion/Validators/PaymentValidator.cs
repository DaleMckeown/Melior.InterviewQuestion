using Melior.InterviewQuestion.Types;

namespace Melior.InterviewQuestion.Validators
{
    public class PaymentValidator
    {
        /// <summary>
        /// Determines whether an account and payment request are valid.
        /// </summary>
        /// <param name="account">The account to validate.</param>
        /// <param name="request">The request to validate.</param>
        /// <returns>A MakePaymentResult indicating the success of the validation.</returns>
        public MakePaymentResult ValidPayment(Account account, MakePaymentRequest request)
        {
            var result = new MakePaymentResult();
            if (account != null)
            {
                if (request.PaymentScheme == PaymentScheme.Bacs)
                {
                    result.Success = ValidBacsPayment(account);
                }
                else if (request.PaymentScheme == PaymentScheme.FasterPayments)
                {
                    result.Success = ValidFasterPayment(account, request);
                }
                else if (request.PaymentScheme == PaymentScheme.Chaps)
                {
                    result.Success = ValidChapsPayment(account);
                }
            }
            else
            {
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Determines whether a 'Bacs' payment method is valid for this account.
        /// </summary>
        /// <param name="account">The account to validate.</param>
        /// <returns>A boolean indicating success.</returns>
        private bool ValidBacsPayment(Account account)
        {
            bool success = false;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Determines whether a 'FasterPayment' payment method is valid for this account.
        /// </summary>
        /// <param name="account">The account to validate.</param>
        /// <param name="request">The request to validate.</param>
        /// <returns>A boolean indicating success.</returns>
        private bool ValidFasterPayment(Account account, MakePaymentRequest request)
        {
            bool success = false;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) || account.Balance < request.Amount)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Determines whether a 'Chaps' payment method is valid for this account.
        /// </summary>
        /// <param name="account">The account to validate.</param>
        /// <returns>A boolean indicating success.</returns>
        private bool ValidChapsPayment(Account account)
        {
            bool success = false;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) || account.Status != AccountStatus.Live)
            {
                success = false;
            }
            return success;
        }
    }
}