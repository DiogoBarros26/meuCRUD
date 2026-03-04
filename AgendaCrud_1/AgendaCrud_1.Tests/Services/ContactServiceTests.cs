using AgendaCrud_1.Application.Services;
using AgendaCrud_1.Domain.Entities;
using AgendaCrud_1.Infrastructure.Repositories;
using Moq;
using Xunit;

public class ContactServiceTests
{
    [Fact]
    public async Task CreateAsync_DeveGerarId_QuandoContatoValido()
    {
        // Arrange
        var repositoryMock = new Mock<IContactRepository>();
        var service = new ContactService(repositoryMock.Object);

        var contact = new Contact
        {
            Name = "João",
            Email = "joao@email.com",
            Phone = "999999999"
        };

        // Act
        await service.CreateAsync(contact);

        // Assert
        Assert.NotEqual(Guid.Empty, contact.Id);
        repositoryMock.Verify(r => r.AddAsync(contact), Times.Once);
    }
}
