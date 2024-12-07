using ToDoList.Application.Contracts.Persistence;

namespace ToDoList.Persistance.Repositories
{
    public class AddressRepository : GenericRepository<Domain.Address>, IAddressRepository
    {
        private readonly ToDoListDbContext _context;
        public AddressRepository(ToDoListDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
