using WebApi.Domain.Service;
using WebApi.Security.Handlers.Interfaces;

namespace WebApi.Security.Middleware
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;
         public JwtMiddleware(RequestDelegate next)
        {
              _next = next;
        }
        public async Task Invoke(HttpContext context,IUserService userService,IJwtHandler jwtHandler) {

            
                string token = getToken(context.Request);
                Console.WriteLine(token);
                if(!string.IsNullOrEmpty(token))
                {
                     Console.WriteLine("token");
                    if (jwtHandler.ValidateToken(token))
                    {
                        Console.WriteLine("token validado");
                        string username = jwtHandler.GetUserNameFromToken(token);
                        string[]roles = jwtHandler.GetRolesFromToken(token);                      
                        var user = await userService.GetUserByUserName(username);
                        Console.WriteLine(user.Username);
                        if (user != null) {
                            context.Items["User"] = user;
                            context.Items["Roles"] = roles;
                    }
                    }

                }
            
            
            await _next(context);



        
        }



        private string getToken(HttpRequest request)
        {
            string header = request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(header) && header.StartsWith("Bearer "))
            {
                return header.Replace("Bearer ", "");
            }
            return null;
        }


    }
}
