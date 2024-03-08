using IServ.ETL.Services;

namespace IServ.API
{
    public class MyMiddlewareClass
    {
        private RequestDelegate _next;
        public MyMiddlewareClass(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ETLService eTLService)
        {
            string configvalue1 = System.Configuration.ConfigurationManager.AppSettings["numberThreads"];
            string configvalue2 = System.Configuration.ConfigurationManager.AppSettings["where"];

            if (!bool.TryParse(configvalue2, out bool where))
                throw new ArgumentException(nameof(configvalue2));

            InitializeHelper.Initialize(where);

            int.TryParse(configvalue1, out var numberThreads);

            Console.WriteLine("MyMiddlewareClass is now Invoked");
            eTLService.Initialize(numberThreads);
            await _next.Invoke(context);
        }
    }
}
