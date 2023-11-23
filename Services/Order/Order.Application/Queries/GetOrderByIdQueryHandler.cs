using MediatR;
using Order.Application.Models;
using Order.Domain.Models.Orders;

namespace Order.Application.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, QueryResult<OrderDTO>>
    {
        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            this._repository = repository;
        }

        public async Task<QueryResult<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetAsync(request.Id);

            return new QueryResult<OrderDTO>(result: order.ToOrderDTO(), type: QueryResultTypeEnum.Success);
        }

        #region Private members
        private readonly IOrderRepository _repository;
        #endregion
    }
}
