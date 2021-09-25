using System.Threading.Tasks;
using DatabaseConnection.Models;

namespace DatabaseConnection.Data {
    public class RContext {
        private readonly DBContext _db;

        //Here you can see my example on getting data for my family tree I am working on.
        //We start out by making a refrence to the to model using the Repository<Model> like I have done underneath.
        //After that we move on to the constructor.
        //public Repository<Member> Member { get; private set; }

        public Repository<Member> Member { get; private set; }
        public Repository<Sex> Sex { get; private set; }
        public Repository<MemberConnection> MemberConnection { get; private set; }
        public Repository<Families> Family { get; private set; }
        public Repository<FamilyConnection> FamilyConnection { get; private set; }
        public Repository<ConnectionType> ConnectionType { get; private set; }

        public RContext(DBContext db) {
            _db = db;

            //Here we inject the DBContext in to the Repository.
            //Member = new Repository<Member>(_db);

            Member = new Repository<Member>(_db);
            Sex = new Repository<Sex>(_db);
            MemberConnection = new Repository<MemberConnection>(_db);
            Family = new Repository<Families>(_db);
            FamilyConnection = new Repository<FamilyConnection>(_db);
            ConnectionType = new Repository<ConnectionType>(_db);
        }



        public void Dispose() => _db.Dispose();

        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     Ssee <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information.
        /// </remarks>
        /// <param name="cancellationToken"> A <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        /// <exception cref="OperationCanceledException"> If the <see cref="CancellationToken" /> is canceled. </exception>
        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
