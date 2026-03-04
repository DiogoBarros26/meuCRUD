using AgendaCrud_1.Domain.Entities;

namespace AgendaCrud_1.Infrastructure.Repositories;

public interface IContactRepository
{
    Task<List<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(Guid id);
    Task AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(Contact contact);
}
