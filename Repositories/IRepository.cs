using System.Collections.Generic;
using System.Threading.Tasks;

namespace DialysisManagement.Repositories
{
    /// <summary>
    /// Repository pattern generico per operazioni CRUD
    /// </summary>
    /// <typeparam name="T">Tipo entità</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Ottiene tutte le entità
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Ottiene un'entità per ID
        /// </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Inserisce una nuova entità
        /// </summary>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Aggiorna un'entità esistente
        /// </summary>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Elimina un'entità per ID
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Conta il numero totale di entità
        /// </summary>
        Task<int> CountAsync();
    }
}
