using AgendaCrud_1.Domain.Entities;
using AgendaCrud_1.Infrastructure.Repositories;

namespace AgendaCrud_1.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Contact?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Contact contact)
    {
        if (string.IsNullOrWhiteSpace(contact.Name))
            throw new Exception("Nome é obrigatório");

        contact.Id = Guid.NewGuid();

        await _repository.AddAsync(contact);
    }

    public async Task UpdateAsync(Guid id, Contact contact)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            throw new Exception("Contato não encontrado");

        existing.Name = contact.Name;
        existing.Email = contact.Email;
        existing.Phone = contact.Phone;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            throw new Exception("Contato não encontrado");

        await _repository.DeleteAsync(existing);
    }
}
