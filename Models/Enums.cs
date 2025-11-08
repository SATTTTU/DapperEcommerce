namespace EcommerceApp.Models
{
    public class Enums
    {
   
        public enum UserRole
        {
            Admin,
            Customer
        }
        public enum OrderStatus
        {
            Pending,
            Processing,
            Shipped,
            Delivered,
            Cancelled
        }
        public enum PaymentMethod
        {
            CreditCard,
            PayPal,
            BankTransfer,
            CashOnDelivery
        }
        public enum ProductStatus
        {
            Active,
            InActive,
            OutOfStock
        }
        public enum AddressType
        {
            Shipping,
            Billing
        }
        public enum PaymentStatus
        {
            Pending,
            Success,
            Failed
        }


    }


}
