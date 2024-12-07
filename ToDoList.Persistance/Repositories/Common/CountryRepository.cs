using ToDoList.Application.Contracts.Persistence.Common;

namespace ToDoList.Persistance.Repositories.Common
{
    public class CountryRepository : GenericRepository<Domain.Common.Country>, ICountryRepository
    {
        private readonly ToDoListDbContext _context;
        public CountryRepository(ToDoListDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
