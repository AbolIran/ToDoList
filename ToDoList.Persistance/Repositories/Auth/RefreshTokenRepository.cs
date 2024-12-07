using ToDoList.Application.Contracts.Persistence.Auth;

namespace ToDoList.Persistance.Repositories.Auth
{
    public class RefreshTokenRepository : GenericRepository<Domain.Auth.RefreshToken>, IRefreshTokenRepository
    {
        private readonly ToDoListDbContext _context;
        public RefreshTokenRepository(ToDoListDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
