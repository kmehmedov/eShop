using Microsoft.AspNetCore.Authentication;
using Yarp.ReverseProxy.Forwarder;

namespace WebMVC
{
    public class MvcAuthHttpTransformer : HttpTransformer
    {
        public override async ValueTask TransformRequestAsync(HttpContext httpContext, HttpRequestMessage proxyRequest, string destinationPrefix, CancellationToken cancellationToken)
        {
            // Set the access token as a bearer token for the outgoing request
            var accessToken = await httpContext.GetTokenAsync("access_token");

            if (accessToken is not null)
            {
                proxyRequest.Headers.Authorization = new("Bearer", accessToken);
            }

            await base.TransformRequestAsync(httpContext, proxyRequest, destinationPrefix, cancellationToken);
        }
    }
}
