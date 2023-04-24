using Microsoft.EntityFrameworkCore;
using STCA_DataLib.Data;
using STCA_DataLib.Model;

namespace STCA_WebApp.Services
{
    public class SQLServer_STCA_DbService : ISTCA_DbService
    {
        private readonly STCA_DbContext _dbContext;

        public SQLServer_STCA_DbService(STCA_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateZonaHorariaAsync(ZonaHoraria item)
        {
            _dbContext.Add(item);
            var numRecords = await _dbContext.SaveChangesAsync();
            return numRecords > 0;
        }

        public async Task<ZonaHoraria[]> GetZonasHorariasAsync()
        {
            var x = await _dbContext.ZonaHoraria.ToArrayAsync();
            return x;
        }

    }
}
