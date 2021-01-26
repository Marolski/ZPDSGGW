using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ZPDSGGW.Authentication
{
    public class BasicAuthentication: AuthenticationSchemeOptions { }
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthentication>
    {
        private readonly ICustomAuthenticationManager customAuthenticationManager;

        public CustomAuthenticationHandler(IOptionsMonitor<BasicAuthentication> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, ICustomAuthenticationManager customAuthenticationManager)
            : base(options, logger, encoder, clock)
        {
            this.customAuthenticationManager = customAuthenticationManager;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorizated");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.Fail("Unauthorizated");

            if(!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Unauthorizated");

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if(string.IsNullOrEmpty(token))
                return AuthenticateResult.Fail("Unauthorizated");

            try
            {
                return ValidateToken(token);
            }
            catch (Exception e)
            {

                return AuthenticateResult.Fail("Unauthorizated");
            }
        }

        private AuthenticateResult ValidateToken(string token)
        {
            var validatedToken = customAuthenticationManager.Tokens.FirstOrDefault(t => t.Key == token);
            if (validatedToken.Key == null)
                return AuthenticateResult.Fail("Unauthorizated");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.Value.Item1 ),
                new Claim(ClaimTypes.Role, validatedToken.Value.Item2.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principle = new GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principle, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
