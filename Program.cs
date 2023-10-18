namespace DotnetTest
{
  public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(o => o.Limits.MaxRequestBodySize = 159715200);
                webBuilder.UseStartup<Startup>();
            });
}
}
