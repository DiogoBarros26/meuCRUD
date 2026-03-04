using AgendaCrud_1.Domain.Entities;

namespace AgendaCrud_1.Application.Services;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(Guid id);
    Task CreateAsync(Contact contact);
    Task UpdateAsync(Guid id, Contact contact);
    Task DeleteAsync(Guid id);
}
