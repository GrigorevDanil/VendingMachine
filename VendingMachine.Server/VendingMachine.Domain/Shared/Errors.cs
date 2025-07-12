
namespace VendingMachine.Domain.Shared
{
    public class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? value = null, string? invalidField = null)
            {
                var valueString = value == null ? "" : $"`{value}` ";
                return Error.Validation("VALUE_IS_INVALID", $"Value {valueString}is invalid", invalidField);
            }
            
            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id '{id}'";
                return Error.NotFound("RECORD_NOT_FOUND", $"Record not found{forId}");
            }

        }
        
        public static class Server
        {
            public static Error InternalServer(string message)
            {
                return Error.Validation("INTERNAL_SERVER_ERROR", message);
            }

        }
        
    }
}
