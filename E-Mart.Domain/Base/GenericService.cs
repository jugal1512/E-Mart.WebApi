namespace E_Mart.Domain.Base;
public class GenericService<T> : IGenericService<T> where T : BaseEntity,new()
{
    private readonly IGenericRepository<T> _genericRepository;
    public GenericService(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
    }
    public async Task<T> AddAsync(T entity)
    {
        return await _genericRepository.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _genericRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _genericRepository.GetAllAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        return await _genericRepository.UpdateAsync(entity);
    }
}