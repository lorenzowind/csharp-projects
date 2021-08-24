using Microsoft.Extensions.DependencyInjection;

using ProjectWs03.src.shared.database.contexts;

namespace ProjectWs03.src.shared.database.utils
{
  public class SqlServerService
  {
    public readonly SqlServerContext context;

    public SqlServerService(IServiceCollection serviceCollection) 
    {
      context = serviceCollection
        .BuildServiceProvider()
        .GetRequiredService<SqlServerContext>();
    }
  }
}
