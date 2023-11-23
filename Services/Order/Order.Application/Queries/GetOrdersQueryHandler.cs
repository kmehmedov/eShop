using MediatR;
using Order.Application.Models;
using Order.Domain.Models.Orders;

namespace Order.Application.Queries
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, QueryResult<IEnumerable<OrderDTO>>>
    {
        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<QueryResult<IEnumerable<OrderDTO>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByBuyerIdAsync(request.BuyerId);

            return new QueryResult<IEnumerable<OrderDTO>>(orders.ToOrdersDTO(), QueryResultTypeEnum.Success);
        }

        #region Private members
        private readonly IOrderRepository _orderRepository;
        #endregion
    }
}
