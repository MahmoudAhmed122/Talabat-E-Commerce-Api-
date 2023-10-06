using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.IRepositories;
using Talabat.core.Services;
using Talabat.core.Specification;
using Talabat.Core.Entities;
using Talabat.Core.IGenericRepository;
using Talabat.Core.IRepositories;

namespace Talabat.Service.OrderServices
{
    public class OrderServices : IOrderService
    {
        public IBasketRepository basketRepository { get; }
        public IUnitOfWork UnitOfWork { get; }

        public OrderServices(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            this.basketRepository = basketRepository;
            UnitOfWork = unitOfWork;
        }



        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1- Get basket From basketRepository
            var basket = await basketRepository.GetBasketAsync(basketId);

            //2- Get Selected Items at basket from ProductRepository
            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {

                foreach (var item in basket.Items)
                {

                    var product = await UnitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var orderItem = new OrderItem(product.Id, product.Name, product.PictureUrl, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            // 3- Calculate Subtotal
            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);
            //4- get Delivery Method from DeliveryMethodRepository
            var deliveryMethod = await UnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // 5- Create Order 
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

            // 6- Save Order in Database
            await UnitOfWork.Repository<Order>().Add(order);
            var result = await UnitOfWork.Complete();
            if (result <= 0)
                return null;
            return order;

        } 

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {

            var spec = new OrderSpecifications(buyerEmail , orderId);
            var order = UnitOfWork.Repository<Order>().GetByIdAsyncWithSpecification(spec);
            return order;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = UnitOfWork.Repository<Order>().GetAllAsyncWithSpecification(spec);
            return orders;
        }

       public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await UnitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }
    }
}
