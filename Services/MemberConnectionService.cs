using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseConnection.Models;
using DatabaseConnection.Data;

namespace DatabaseConnection.Services {
    public class MemberConnectionService : BaseService {
        public MemberConnectionService(DBContext databaseContext) : base(databaseContext) { }

        public async Task<List<MemberConnection>> GetMemberConnectionsAsync(int value) {
            Families family = await _databaseContext.Families.FirstOrDefaultAsync(f => f.ID == value);
            List<FamilyConnection> familyConnections = await _databaseContext.FamilyConnections
                .Where(f => f.FamilyID == family.ID)
                .Include(fc => fc.User) //Include adds the user on to the FamilyConnection
                .ToListAsync();

            return (await _databaseContext.MemberConnections
               .Include(mc => mc.ConnectionType)
               .Include(mc => mc.ConnectionType2)
               .Include(mc => mc.User) //Include adds the user on to the MemberConnection
                    .ThenInclude(u => u.Sex) //Include adds the sex on to the User
               .Include(mc => mc.User2)
                    .ThenInclude(u => u.Sex)
               .ToListAsync())
                    .Where(mc => familyConnections.Any(fc => fc.UserID == mc.UserID || fc.UserID == mc.User2ID)).ToList();
        }
    }
}