using MediatR;
using Order.Application.Models;

namespace Order.Application.Queries
{
    public class GetOrdersQuery : IRequest<QueryResult<IEnumerable<OrderDTO>>>
    {
        public string BuyerId { get; set; }
    }
}
