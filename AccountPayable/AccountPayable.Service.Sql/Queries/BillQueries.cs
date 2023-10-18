using System.Diagnostics.CodeAnalysis;

namespace AccountPayable.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class BillQueries
    {
        public static string AllBill=> "SELECT * FROM [Bill] (NOLOCK)";

        public static string BillById => "SELECT * FROM [Bill] (NOLOCK) WHERE [Id] = @Id";

        public static string AddBill =>
            @"INSERT INTO [Bill] ([OrderOf], [AccountId], [VendorId], [Amount], [DueDate], [Paid]) 
				VALUES (@OrderOf, @AccountId, @VendorId, @Amount, @DueDate, @Paid)";

        public static string UpdateBill =>
            @"UPDATE [Bill] 
            SET [OrderOf] = @OrderOf, 
				[AccountId] = @AccountId, 
				[VendorId] = @VendorId, 
				[Amount] = @Amount, 
				[DueDate] = @DueDate
				[Paid] = @Paid
            WHERE [Id] = @Id";

        public static string DeleteBill => "DELETE FROM [Bill] WHERE [Id] = @Id";
    }
}