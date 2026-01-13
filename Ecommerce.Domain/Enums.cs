namespace Ecommerce.Domain
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Paid,
        Cancelled,
        Shipped,
        Delivered
    }

    public enum CardType
    {
        Debit,
        Credit
    }
}