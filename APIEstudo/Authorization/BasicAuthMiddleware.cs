﻿using APIBasicAuthentication.Services;
using System.Net.Http.Headers;
using System.Text;

namespace APIBasicAuthentication.Authorization
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public BasicAuthMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                context.Items["User"]= await userService.Authenticate(username, password);
            }
            catch
            {

             
            }

            await _requestDelegate(context);
        }
    }
}
