using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatabaseConnection.Models;
using DatabaseConnection.Data;

namespace DatabaseConnection.Services {
    public class FamilyService : BaseService {
        public FamilyService(DBContext databaseContext) : base(databaseContext) { }

        public async Task<List<Families>> GetAll() {
            return await _databaseContext.Families.ToListAsync();
        }

        public async Task<Families> Get(int id) {
            if (id > 0) return await _databaseContext.Families.FirstOrDefaultAsync(f => f.ID == id);
            return new();
        }
    }
}