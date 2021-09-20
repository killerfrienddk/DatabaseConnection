using DatabaseConnection.Data;

namespace DatabaseConnection.Services {
   public class BaseService {
        // We use a base service to get the DBContext in every service without writing it multiple times.
        public readonly DBContext _databaseContext;
        public BaseService(DBContext databaseContext) {
            _databaseContext = databaseContext;
        }
    }
}
