﻿using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;

namespace Insurance_agency
{
    public class DisplayAndAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public DisplayAndAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Mặc định display = 1
            context.Session.SetInt32("display", 1);
            context.Session.SetInt32("allbanner", 1);

            var path = context.Request.Path.Value.ToLower();

           
      

            // Nếu URL là admin hoặc home
            if (path.Contains("admin") || path.Contains("home"))
            {
                //var user = new User { full_name = "Thoại",auth_id=4 };
                //context.Session.SetObject("user", user);

                var acc = context.Session.GetObject<User>("user");

                if (acc != null)
                
                {
                    var roleId = acc.auth_id;

                    if (!MiddleWareRepository.Instance.CheckAuthorization(roleId, path))
                    {
                        if (roleId == 4)
                        {
                            context.Response.Redirect("/Home/Index");
                            return;
                        }

                        context.Response.Redirect("/AdminArea/Dashboard");
                        return;
                    }
                }
                else if (path.Contains("admin"))
                {
                    context.Response.Redirect("/Home/Insurance");
                    return;
                }
            }

            // Tiếp tục pipeline
            await _next(context);
        }
    }

}
