using Microsoft.AspNetCore.Mvc;

namespace Notification.Email.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    public EmailController(ILogger<EmailController> logger, IEmailSender sender)
    {
        _logger = logger;
        _sender=sender;
    }

    [HttpPost]
    public async Task<ActionResult> Send()
    {
        await _sender.SendAsync();

        return Ok();
    }

    #region Private members
    private readonly ILogger<EmailController> _logger;
    private readonly IEmailSender _sender;
    #endregion
}
