using STCA_DataLib.Model;

namespace STCA_DataLib.Repositories
{
    /// <summary>
    /// ISCTA_UnitOfWork defines the contract for a middle layer between the Bussines Logic and the Darasource.
    /// We this interface we can deploy a mok class to perform Unit or Integrated tests,
    /// and deploy a clase to interact with a real datasource.
    /// The contact guarantees we should have some repository for each Entity we want to manage in out App.
    /// </summary>
    public interface ISCTA_UnitOfWork
    {
        IGenericRepository<ZonaHoraria> ZonaHorariaRepository { get; }

        void Dispose();

        Task<int> SaveAsync();

    }
}