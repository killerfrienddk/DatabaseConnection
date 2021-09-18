using DatabaseConnection.Data;

namespace DatabaseConnection.Services {
   public class BaseService {
        public readonly DBContext _databaseContext;
        public BaseService(DBContext databaseContext) {
            _databaseContext = databaseContext;
        }
    }
}
