using ToDoList.Application.Contracts.Persistence.Common;

namespace ToDoList.Persistance.Repositories.Common
{
    public class CityRepository : GenericRepository<Domain.Common.City>, ICityRepository
    {
        private readonly ToDoListDbContext _context;
        public CityRepository(ToDoListDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
