using RecruitmentApp.Data;
using RecruitmentApp.Shared.Base.abstraction;

namespace RecruitmentApp.Shared.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        public RepositoryBase(DataContext dataContext)
        {
            _context = dataContext;
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
