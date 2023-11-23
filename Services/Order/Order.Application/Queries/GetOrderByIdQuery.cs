using MediatR;
using Order.Application.Models;

namespace Order.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<QueryResult<OrderDTO>>
    {
        public int Id { get; set; }
    }
}
