using System.Diagnostics.CodeAnalysis;

namespace AccountPayable.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class PaymentQueries
    {
        public static string AllPayment=> "SELECT * FROM [Payment] (NOLOCK)";

        public static string PaymentById => "SELECT * FROM [Payment] (NOLOCK) WHERE [Id] = @Id";

        public static string AddPayment =>
            @"INSERT INTO [Payment] ([BillId], [AccountId], [Amount], [PaymentDate], [PaymentMethodId]) 
				VALUES (@BillId, @AccountId, @Amount, @PaymentDate, @PaymentMethodId)";

        public static string UpdatePayment =>
            @"UPDATE [Payment] 
            SET [BillId] = @BillId, 
				[AccountId] = @AccountId, 
				[Amount] = @Amount, 
				[PaymentDate] = @PaymentDate
				[PaymentMethodId] = @PaymentMethodId
            WHERE [Id] = @Id";

        public static string DeletePayment => "DELETE FROM [Payment] WHERE [Id] = @Id";
    }
}