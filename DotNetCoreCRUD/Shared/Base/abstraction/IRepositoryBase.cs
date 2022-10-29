namespace RecruitmentApp.Shared.Base.abstraction
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
    }
}
