using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Events;
using System.Xml.Linq;

namespace Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string OrderNumber { get; private set; }

    public DateTime OrderDate { get; private set; }

    public string CustomerName { get; private set; }

    public Email CustomerEmail { get; private set; }

    public Address Address { get; private set; }

    public OrderStatus Status { get; private set; }

    protected Order()
    {

    }

    public Order(string orderNumber,
                    DateTime orderDate,
                    string customerName,
                    Email customerEmail,
                    Address address) : base()
    {
        OrderNumber= Guard.Against.Null(orderNumber, nameof(orderNumber));
        OrderDate= Guard.Against.Null(orderDate, nameof(orderDate));
        CustomerName=Guard.Against.NullOrEmpty(customerName, nameof(customerName));
        CustomerEmail=Guard.Against.Null(customerEmail, nameof(customerEmail));
        Address= Guard.Against.Null(address, nameof(address));
        Status = OrderStatus.New;

        AddDomainEvent(new OrderCreatedEvent(this));
    }

    public void CancelOrder()
    {
        this.Status = OrderStatus.Canceled;
    }

    public void MakeOrderProcessing()
    {
        this.Status = OrderStatus.Processing;
    }

    public Result<bool> AddOrderItem(OrderItem orderItem)
    {
        if (orderItem == null)
            return Result.Failure<bool>("Items is null!");

        var existItem = items.FirstOrDefault(a => a.Product == orderItem.Product);

        if (existItem != null)
        {
            existItem.Quantity += orderItem.Quantity;
        }
        else
        {
            items.Add(orderItem);
        }

        return Result.Success(true);
    }

    public Result<bool> AddListOfOrderItem(List<OrderItem> orderItems)
    {
        if (orderItems == null)
            return Result.Failure<bool>("Items is empty");

        items.AddRange(orderItems);

        return Result.Success(true);
    }

    public Result<bool> EditOrder(string customerName,
                                    Email customerEmail,
                                    Address address)
    {

        CustomerName=Guard.Against.NullOrEmpty(customerName, nameof(customerName));
        CustomerEmail=Guard.Against.Null(customerEmail, nameof(customerEmail));
        Address= Guard.Against.Null(address, nameof(address));

        if (CustomerEmail != customerEmail)
        {
            AddDomainEvent(new OrderEmailChangedEvent(Id, customerEmail));
        }

        CustomerName=customerName;

        CustomerEmail=customerEmail;

        Address=address;

        AddDomainEvent(new OrderEditedEvent(this));

        return Result.Success(true);
    }

    public Result<bool> RemoveOrderItem(OrderItem orderItem)
    {
        if (!items.Any())
        {
            return Result.Failure<bool>("There is no order item in the list");
        }

        items.Remove(orderItem);

        return Result.Success(true);
    }


    private readonly List<OrderItem> items = new List<OrderItem>();

    public virtual IReadOnlyList<OrderItem> Items => items.ToList();

}
