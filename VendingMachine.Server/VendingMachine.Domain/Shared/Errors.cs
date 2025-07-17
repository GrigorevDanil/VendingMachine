
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
        
        public static class Order
        {
            public static Error ProductNotEnough(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id '{id}'";
                return Error.NotFound("PRODUCT_NOT_ENOUGH", $"Product{forId} not enough");
            }   
            

        }
        
        public static class Server
        {
            public static Error InternalServer(string message)
            {
                return Error.Validation("INTERNAL_SERVER_ERROR", message);
            }

        }
        
        public static class File
        {
            public static Error FileIsInvalid(string message)
            {
                return Error.Validation("FILE_IS_INVALID", message);
            }
            
            public static Error NotFound(string fileName)
            {
                return Error.NotFound("FILE_NOT_FOUND", $"File '{fileName}' not found");
            }
            
            public static Error NotProvide()
            {
                return Error.NotFound("FILE_NOT_PROVIDE", "File not provide");
            }
            
            public static Error NotImage()
            {
                return Error.NotFound("FILE_NOT_IMAGE", $"File not image");
            }

            public static Error NotExcel()
            {
                return Error.NotFound("FILE_NOT_EXCEL", $"File not excel file");
            }
        }
        
    }
}
