using Microsoft.EntityFrameworkCore;
using Shared;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_WebApp.Extensions;
using STCA_WebApp.ModelsDTO;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;

namespace STCA_WebApp.Services
{
    public class MSSQL_STCA_DbService : ISTCA_DbService
    {
        private readonly MSSQL_STCA_DbContext _dbContext;

        public MSSQL_STCA_DbService(MSSQL_STCA_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateZonaHorariaAsync(ZonaHoraria item)
        {
            _dbContext.Add(item);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteZonaHorariaAsync(int id)
        {
            var item = await _dbContext.ZonaHoraria.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null)
                return false;

            _dbContext.Remove(item);
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<ZonaHorariaListDTO[]> GetZonasHorariasAsync(ZonaHorariaOrderbyOptions? orderbyOptions = ZonaHorariaOrderbyOptions.NOMBRE_ASC)
        {
            var items = await _dbContext.ZonaHoraria.AsNoTracking()
                .MapZonaHorariaToDto()
                .OrderBy(orderbyOptions)
                .Take(100)
                .ToArrayAsync();

            return items;

        }

    }
}
