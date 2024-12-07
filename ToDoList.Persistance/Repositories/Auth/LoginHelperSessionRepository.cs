using ToDoList.Application.Contracts.Persistence.Auth;

namespace ToDoList.Persistance.Repositories.Auth
{
    public class LoginHelperSessionRepository : GenericRepository<Domain.Auth.LoginHelperSession>, ILoginHelperSessionRepository
    {
        private readonly ToDoListDbContext _context;
        public LoginHelperSessionRepository(ToDoListDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
