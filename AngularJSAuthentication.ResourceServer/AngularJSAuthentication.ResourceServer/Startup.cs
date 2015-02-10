using AngularJSAuthentication.ResourceServer.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


[assembly: OwinStartup(typeof(AngularJSAuthentication.ResourceServer.Startup))]
namespace AngularJSAuthentication.ResourceServer
{
    
        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                HttpConfiguration config = new HttpConfiguration();

                config.MapHttpAttributeRoutes();

                ConfigureOAuth(app);

                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

                app.UseWebApi(config);

            }

            public void ConfigureOAuth(IAppBuilder app)
            {
                var issuer = "http://localhost:51188";
                var audience = "53b489f7da064e70af2996717a6d3639";
                var secret = TextEncodings.Base64Url.Decode("O19MzWktkKXd-oIUnE0mxFbTzzsfZXVRHpfswcGTjzw");

                // Api controllers with an [Authorize] attribute will be validated with JWT
                app.UseJwtBearerAuthentication(
                    new JwtBearerAuthenticationOptions
                    {
                        AuthenticationMode = AuthenticationMode.Active,
                        AllowedAudiences = new[] { audience },
                        IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                    });

            }
    }
}