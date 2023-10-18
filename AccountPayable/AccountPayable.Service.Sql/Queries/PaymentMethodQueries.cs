using System.Diagnostics.CodeAnalysis;

namespace AccountPayable.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class PaymentMethodQueries : ICrudQueries
    {
        public static string AllPaymentMethod=> "SELECT * FROM [PaymentMethod] (NOLOCK)";

        public static string PaymentMethodById => "SELECT * FROM [PaymentMethod] (NOLOCK) WHERE [Id] = @Id";

        public static string AddPaymentMethod =>
            @"INSERT INTO [PaymentMethod] ([DisplayName]) 
				VALUES (@DisplayName)";

        public static string UpdatePaymentMethod =>
            @"UPDATE [PaymentMethod] 
            SET [DisplayName] = @DisplayName, 
            WHERE [Id] = @Id";

        public static string DeletePaymentMethod => "DELETE FROM [PaymentMethod] WHERE [Id] = @Id";

        public string Add()
        {
            return AddPaymentMethod;
        }

        public string Delete()
        {
            return DeletePaymentMethod;
        }

        public string GetAll()
        {
            return AllPaymentMethod;
        }

        public string GetById()
        {
            return PaymentMethodById;
        }

        public string Update()
        {
            return UpdatePaymentMethod;
        }
    }
}