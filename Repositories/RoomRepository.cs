using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "rooms")
        {
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            const string sql = "SELECT * FROM rooms ORDER BY nome_sala";
            return await QueryAsync(sql);
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM rooms WHERE room_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Room>> GetActiveRoomsAsync()
        {
            const string sql = "SELECT * FROM rooms WHERE attiva = TRUE ORDER BY nome_sala";
            return await QueryAsync(sql);
        }

        public async Task<int> InsertAsync(Room entity)
        {
            const string sql = @"
                INSERT INTO rooms (nome_sala, numero_postazioni, piano, note, attiva)
                VALUES (@NomeSala, @NumeroPostazioni, @Piano, @Note, @Attiva);
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(Room entity)
        {
            const string sql = @"
                UPDATE rooms SET
                    nome_sala = @NomeSala, numero_postazioni = @NumeroPostazioni,
                    piano = @Piano, note = @Note, attiva = @Attiva
                WHERE room_id = @RoomId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM rooms WHERE room_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM rooms";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
