using System;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_DataLib.Repositories;

namespace STCA_DataLib.Repositories
{
    public class SCTA_UnitOfWork : ISCTA_UnitOfWork, IDisposable
    {
        private MSSQL_STCA_DbContext context;

        private IGenericRepository<ZonaHoraria> zonaHorariaRepository;

        public IGenericRepository<ZonaHoraria> ZonaHorariaRepository
        {
            get
            {
                if (this.zonaHorariaRepository == null)
                    this.zonaHorariaRepository = new GenericRepository<ZonaHoraria>(context);
                return zonaHorariaRepository;
            }
        }

        public SCTA_UnitOfWork(MSSQL_STCA_DbContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}