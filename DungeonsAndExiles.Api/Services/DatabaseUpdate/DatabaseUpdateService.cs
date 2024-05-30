using DungeonsAndExiles.Api.Data;

namespace DungeonsAndExiles.Api.Services.DatabaseUpdate
{
    public class DatabaseUpdateService : IDatabaseUpdateService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseUpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UpdateDatabase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var records = dbContext.Players.ToList();


                foreach (var record in records)
                {
                    record.Stamina = 20;
                }
                dbContext.SaveChanges();
            }
        }
    }
}
