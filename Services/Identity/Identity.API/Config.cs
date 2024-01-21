using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity.API;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("webappgateway"),
            new ApiScope("order"),
            new ApiScope("shoppingcart"),
            new ApiScope("notification.signalr")
        };

    public static IEnumerable<ApiResource> ApiResources =>
                new List<ApiResource>
            {
                new ApiResource("webappgateway", "Web app gateway"),
                new ApiResource("order", "Order"),
                new ApiResource("shoppingcart", "Shopping cart"),
                new ApiResource("notification.signalr", "Notification signalR")
            };



    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        return new Client[]
        {
            new Client
                {
                    ClientId = "mvc.client",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = $"{configuration["MvcClient"]}",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = false,
                    RedirectUris = new List<string>
                    {
                        $"{configuration["MvcClient"]}/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{configuration["MvcClient"]}/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "webappgateway",
                        "order",
                        "shoppingcart",
                        "notification.signalr"
                    }
                }
        };
    }
}
