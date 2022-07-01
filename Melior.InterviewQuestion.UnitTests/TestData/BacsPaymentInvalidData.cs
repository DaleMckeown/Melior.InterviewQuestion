using Melior.InterviewQuestion.Types;
using System.Collections;
using System.Collections.Generic;

namespace Melior.InterviewQuestion.UnitTests
{
    public class BacsPaymentInvalidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, PaymentScheme.Bacs, AllowedPaymentSchemes.Chaps };
            yield return new object[] { 1, PaymentScheme.Bacs, AllowedPaymentSchemes.FasterPayments };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}