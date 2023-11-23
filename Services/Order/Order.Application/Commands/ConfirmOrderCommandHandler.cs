using MediatR;
using Order.Application.IntegrationEvents;
using Order.Application.IntegrationEvents.Events;
using Order.Application.Models;
using Order.Domain.Models.Orders;

namespace Order.Application.Commands
{
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, CommandResult<bool>>
    {
        public ConfirmOrderCommandHandler(IOrderRepository repository, IOrderIntegrationEventService integrationEventService)
        {
            this._repository=repository;
            this._integrationEventService=integrationEventService;
        }

        public async Task<CommandResult<bool>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            //var order = await _repository.GetAsync(request.Id);
            //order.Confirm();
            //await _repository.UnitOfWork.SaveChangesAsync();
            //var integrationEvent = new OrderConfirmedIntegrationEvent(order.BuyerId, order.OrderItems);
            //_integrationEventService.PublishThroughEventBus(integrationEvent);
            return new CommandResult<bool>(true, CommandResultTypeEnum.Success);
        }

        #region Private members
        private readonly IOrderRepository _repository;
        private readonly IOrderIntegrationEventService _integrationEventService;
        #endregion
    }
}
