using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Orders.Frontend.AuthenticationProviders;

public class AuthenticationProviderTest : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        //await Task.Delay(1000); //esto es para probar q funciona la autenticacion
        var anonimous = new ClaimsIdentity();
        var user = new ClaimsIdentity(authenticationType: "test");
        var admin = new ClaimsIdentity(
                    [
                        new("FirstName", "Ariel"),
                        new("LastName", "Quintana"),
                        new(ClaimTypes.Name, "ariel@yopmail.com"),
                        new(ClaimTypes.Role, "Admin")
                    ],
                    authenticationType: "test");

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
    }
}