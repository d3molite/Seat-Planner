using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Planner.Db.Utils;

public static class DatabaseCreator
{
	public static void EnsureDbCreated<T>(IServiceProvider provider) where T: DbContext
	{
		var serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
		
		var context = serviceScope.ServiceProvider.GetRequiredService<T>();
		context.Database.Migrate();
	}
}