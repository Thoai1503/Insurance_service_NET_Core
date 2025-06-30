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

            // Nếu URL thuộc nhóm ẩn display
            if (path.Contains("cart") || path.Contains("blog") || path.Contains("contact")
                || path.Contains("checkoutpage") || path.Contains("shop")
                || path.Contains("orderhistory") || path.Contains("orderdetail"))
            {
                context.Session.SetInt32("display", 0);
            }

            // Nếu URL là admin hoặc home
            if (path.Contains("admin") || path.Contains("home"))
            {
                //var acc = context.Session.GetObject<MemberView>("acc"); // Xem bên dưới phần Extension

                //if (acc != null)
                //{
                //    var roleId = acc.RoleId;

                //    if (!MiddleWareRepository.Instance.CheckAuthorization(roleId, path))
                //    {
                //        if (roleId == 2)
                //        {
                //            context.Response.Redirect("/Home/Index");
                //            return;
                //        }

                //        context.Response.Redirect("/Admin/Index");
                //        return;
                //    }
                //}
                //else if (path.Contains("admin"))
                //{
                //    context.Response.Redirect("/Home/Index");
                //    return;
                //}
            }

            // Tiếp tục pipeline
            await _next(context);
        }
    }

}
