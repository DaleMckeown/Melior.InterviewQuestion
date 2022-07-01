using Melior.InterviewQuestion.Types;
using System.Collections;
using System.Collections.Generic;

namespace Melior.InterviewQuestion.UnitTests
{
    public class FasterPaymentInvalidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 10, PaymentScheme.FasterPayments, AllowedPaymentSchemes.Chaps, 100 };
            yield return new object[] { 1, 10, PaymentScheme.FasterPayments, AllowedPaymentSchemes.Bacs, 100 };
            yield return new object[] { 1, 110, PaymentScheme.FasterPayments, AllowedPaymentSchemes.FasterPayments, 100 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}