using Melior.InterviewQuestion.Types;
using System.Collections;
using System.Collections.Generic;

namespace Melior.InterviewQuestion.UnitTests
{
    public class ChapsPaymentInvalidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, PaymentScheme.Chaps, AllowedPaymentSchemes.Bacs, AccountStatus.Live };
            yield return new object[] { 1, PaymentScheme.Chaps, AllowedPaymentSchemes.FasterPayments, AccountStatus.Live };
            yield return new object[] { 1, PaymentScheme.Chaps, AllowedPaymentSchemes.Chaps, AccountStatus.Disabled };
            yield return new object[] { 1, PaymentScheme.Chaps, AllowedPaymentSchemes.Chaps, AccountStatus.InboundPaymentsOnly };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}