using MediatR;
using Order.Application.Models;

namespace Order.Application.Commands
{
    public class ConfirmOrderCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
    }
}
