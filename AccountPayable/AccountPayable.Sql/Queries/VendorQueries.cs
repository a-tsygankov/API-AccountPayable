using System.Diagnostics.CodeAnalysis;

namespace AccountPayable.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class VendorQueries : ICrudQueries
    {
        public static string AllVendor=> "SELECT * FROM [Vendor] (NOLOCK)";

        public static string VendorById => "SELECT * FROM [Vendor] (NOLOCK) WHERE [Id] = @Id";

        public static string AddVendor =>
            @"INSERT INTO [Vendor] ([DisplayName]) 
				VALUES (@DisplayName)";

        public static string UpdateVendor =>
            @"UPDATE [Vendor] 
            SET [DisplayName] = @DisplayName, 
            WHERE [Id] = @Id";

        public static string DeleteVendor => "DELETE FROM [Vendor] WHERE [Id] = @Id";

        public string Add()
        {
            return AddVendor;
        }

        public string Delete()
        {
            return DeleteVendor;
        }

        public string GetAll()
        {
            return AllVendor;
        }

        public string GetById()
        {
            return VendorById;
        }

        public string Update()
        {
            return UpdateVendor;
        }
    }
}