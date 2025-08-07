namespace SchoolMedicalSystem.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // todo push notification & writing log
                Console.WriteLine("========== GlobalExceptionMiddleware - System exception ==========");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                Console.WriteLine("========== GlobalExceptionMiddleware - End of exception ==========");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    ex.Message,
                    InnerMessage = ex.InnerException?.Message
                });
            }
        }
    }

}
