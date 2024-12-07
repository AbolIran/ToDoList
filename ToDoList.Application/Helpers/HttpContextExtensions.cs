using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Helpers
{
    public static class HttpContextExtensions
    {
        public static string GetAuthToken(this HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            string token = null;

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                token = authHeader.Substring("Bearer ".Length);
            }

            return token;
        }
    }
}
