
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

            public static Error ArrayIsEmpty(string? arrayName = null)
            {
                var withName = arrayName == null ? "" : $"with name '{arrayName}' ";
                return Error.Validation("ARRAY_IS_EMPTY", $"Array {withName}is empty");
            }
        }
        
        public static class Order
        {
            public static Error ProductNotEnough(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id '{id}'";
                return Error.Failure("PRODUCT_NOT_ENOUGH", $"Product{forId} not enough");
            }   
            
            public static Error CoinsNotEnoughForPayment()
            {
                return Error.Failure("COINS_NOT_ENOUGH_FOR_PAYMENT", $"Coins not  enough for payment");
            }   
            
            public static Error NotEnoughAvailableCoins()
            {
                return Error.Failure("NOT_ENOUGH_AVAILABLE_COINS", "The vending machine doesn't have enough coins to give change");
            }
            
            public static Error OrderAlreadyPayment()
            {
                return Error.Failure("ORDER_ALREADY_PAYMENT", "Order already payment");
            }
        }
        
        public static class Server
        {
            public static Error InternalServer(string message)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", message);
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
                return Error.Failure("FILE_NOT_PROVIDE", "File not provide");
            }
            
            public static Error NotImage()
            {
                return Error.Failure("FILE_NOT_IMAGE", $"File not image");
            }

            public static Error NotExcel()
            {
                return Error.Failure("FILE_NOT_EXCEL", $"File not excel file");
            }
            
            public static Error ExtensionNotSupport()
            {
                return Error.Validation("EXTENSION_NOT_SUPPORT", "Extension not support");
            }
        }

        public static class Url
        {
            public static Error InvalidUrlFormat()
            {
                return Error.Validation("INVALID_URL_FORMAT", "Url format is invalid");
            }
        }
        
        public static class Session
        {
            public static Error SessionIsBusy()
            {
                return Error.Validation("SESSION_IS_BUSY", "Session is busy");
            }
        }

    }
}
