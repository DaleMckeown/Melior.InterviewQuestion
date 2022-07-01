using AutoFixture;
using FluentAssertions;
using Melior.InterviewQuestion.Data;
using Melior.InterviewQuestion.Services;
using Melior.InterviewQuestion.Types;
using Melior.InterviewQuestion.Validators;
using NSubstitute;
using System;
using Xunit;

namespace Melior.InterviewQuestion.UnitTests
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _paymentServiceTest;
        private readonly IAccountDataStore _accountDataStore = Substitute.For<IAccountDataStore>();
        private readonly IFixture _fixture = new Fixture();

        public PaymentServiceTests()
        {
            _paymentServiceTest = new PaymentService(_accountDataStore, new PaymentValidator());
        }

        [Theory]
        [ClassData(typeof(BacsPaymentInvalidData))]
        public void MakePayment_ShouldNotMakePayment_WhenBacsIsNotValid(
            string debtorAccountNumber, PaymentScheme paymentScheme, AllowedPaymentSchemes allowedPaymentSchemes)
        {
            // Arrange
            MakePaymentRequest makePaymentRequest = _fixture.Build<MakePaymentRequest>()
                .With(a => a.DebtorAccountNumber, debtorAccountNumber)
                .With(a => a.PaymentScheme, paymentScheme)
                .Create();

            Account account = _fixture.Build<Account>()
                .With(a => a.AccountNumber, debtorAccountNumber)
                .With(a => a.AllowedPaymentSchemes, allowedPaymentSchemes)
                .Create();

            _accountDataStore.GetAccount(debtorAccountNumber).Returns(account);

            // Act
            var result = _paymentServiceTest.MakePayment(makePaymentRequest);

            // Assert
            result.Success.Should().Be(false);
            _accountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
        }

        [Theory]
        [ClassData(typeof(FasterPaymentInvalidData))]
        public void MakePayment_ShouldNotMakePayment_WhenFasterPaymentsIsNotValid(
            string debtorAccountNumber, decimal amount, PaymentScheme paymentScheme, AllowedPaymentSchemes allowedPaymentSchemes, decimal balance)
        {
            // Arrange
            MakePaymentRequest makePaymentRequest = _fixture.Build<MakePaymentRequest>()
                .With(a => a.DebtorAccountNumber, debtorAccountNumber)
                .With(a => a.Amount, amount)
                .With(a => a.PaymentScheme, paymentScheme)
                .Create();

            Account account = _fixture.Build<Account>()
                .With(a => a.AccountNumber, debtorAccountNumber)
                .With(a => a.Balance, balance)
                .With(a => a.AllowedPaymentSchemes, allowedPaymentSchemes)
                .Create();

            _accountDataStore.GetAccount(debtorAccountNumber).Returns(account);

            // Act
            var result = _paymentServiceTest.MakePayment(makePaymentRequest);

            // Assert
            result.Success.Should().Be(false);
            _accountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
        }

        [Theory]
        [ClassData(typeof(ChapsPaymentInvalidData))]
        public void MakePayment_ShouldNotMakePayment_WhenChapsIsNotValid(
           string debtorAccountNumber, PaymentScheme paymentScheme, AllowedPaymentSchemes allowedPaymentSchemes, AccountStatus accountStatus)
        {
            // Arrange
            MakePaymentRequest makePaymentRequest = _fixture.Build<MakePaymentRequest>()
                .With(a => a.DebtorAccountNumber, debtorAccountNumber)
                .With(a => a.PaymentScheme, paymentScheme)
                .Create();

            Account account = _fixture.Build<Account>()
                .With(a => a.AccountNumber, debtorAccountNumber)
                .With(a => a.AllowedPaymentSchemes, allowedPaymentSchemes)
                .With(a => a.Status, accountStatus)
                .Create();

            _accountDataStore.GetAccount(debtorAccountNumber).Returns(account);

            // Act
            var result = _paymentServiceTest.MakePayment(makePaymentRequest);

            // Assert
            result.Success.Should().Be(false);
            _accountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
        }

        [Fact]
        public void MakePayment_ShouldThrowNullReferenceException_WhenPaymentRequestIsNull()
        {
            // Arrange
            string debtorAccountNumber = "1";
            AllowedPaymentSchemes allowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            MakePaymentRequest? makePaymentRequest = null;

            Account account = _fixture.Build<Account>()
                .With(a => a.AccountNumber, debtorAccountNumber)
                .With(a => a.AllowedPaymentSchemes, allowedPaymentSchemes)
                .Create();

            // Act
            Action action = () => _paymentServiceTest.MakePayment(makePaymentRequest);

            // Assert
            action.Should().Throw<NullReferenceException>();
        }
    }
}