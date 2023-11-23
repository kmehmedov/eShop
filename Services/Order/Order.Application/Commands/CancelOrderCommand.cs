using MediatR;
using Order.Application.Models;

namespace Order.Application.Commands
{
    public class CancelOrderCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
    }
}
