using ToDoList.Application.Contracts.Persistence.Common;

namespace ToDoList.Persistance.Repositories.Common
{
    public class StateRepository : GenericRepository<Domain.Common.State>, IStateRepository
    {
        private readonly ToDoListDbContext _context;
        public StateRepository(ToDoListDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
