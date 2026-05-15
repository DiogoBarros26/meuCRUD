using AgendaCrud_1.Application.Services;
using AgendaCrud_1.Domain.Entities;
using AgendaCrud_1.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace AgendaCrud_1.Tests;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _repositoryMock;
    private readonly ContactService _service;

    public ContactServiceTests()
    {
        _repositoryMock = new Mock<IContactRepository>();
        _service = new ContactService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarListaDeContatos()
    {
        var contatos = new List<Contact>
        {
            new Contact { Id = Guid.NewGuid(), Name = "João", Email = "joao@email.com", Phone = "81999990000" },
            new Contact { Id = Guid.NewGuid(), Name = "Maria", Email = "maria@email.com", Phone = "81988880000" }
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contatos);

        var resultado = await _service.GetAllAsync();

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task GetByIdAsync_QuandoContatoExiste_DeveRetornarContato()
    {
        var id = Guid.NewGuid();
        var contato = new Contact { Id = id, Name = "João", Email = "joao@email.com", Phone = "81999990000" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contato);

        var resultado = await _service.GetByIdAsync(id);

        Assert.NotNull(resultado);
        Assert.Equal("João", resultado.Name);
    }

    [Fact]
    public async Task GetByIdAsync_QuandoContatoNaoExiste_DeveRetornarNull()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Contact?)null);

        var resultado = await _service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(resultado);
    }

    [Fact]
    public async Task CreateAsync_QuandoDadosValidos_DeveCriarContato()
    {
        var novoContato = new Contact { Name = "Ana", Email = "ana@email.com", Phone = "81977770000" };

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);

        await _service.CreateAsync(novoContato);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Contact>()), Times.Once);
        Assert.NotEqual(Guid.Empty, novoContato.Id);
    }

    [Fact]
    public async Task CreateAsync_QuandoNomeVazio_DeveLancarExcecao()
    {
        var contatoSemNome = new Contact { Name = "", Email = "teste@email.com", Phone = "81999990000" };

        var excecao = await Assert.ThrowsAsync<Exception>(() => _service.CreateAsync(contatoSemNome));
        Assert.Equal("Nome é obrigatório", excecao.Message);
    }

    [Fact]
    public async Task CreateAsync_QuandoNomeNulo_DeveLancarExcecao()
    {
        var contatoNomeNulo = new Contact { Name = null!, Email = "teste@email.com", Phone = "81999990000" };

        var excecao = await Assert.ThrowsAsync<Exception>(() => _service.CreateAsync(contatoNomeNulo));
        Assert.Equal("Nome é obrigatório", excecao.Message);
    }

    [Fact]
    public async Task UpdateAsync_QuandoContatoExiste_DeveAtualizarDados()
    {
        var id = Guid.NewGuid();
        var contatoExistente = new Contact { Id = id, Name = "João", Email = "joao@email.com", Phone = "81999990000" };
        var dadosNovos = new Contact { Name = "João Silva", Email = "joaosilva@email.com", Phone = "81988880000" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contatoExistente);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);

        await _service.UpdateAsync(id, dadosNovos);

        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_QuandoContatoNaoExiste_DeveLancarExcecao()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Contact?)null);

        var excecao = await Assert.ThrowsAsync<Exception>(
            () => _service.UpdateAsync(Guid.NewGuid(), new Contact())
        );
        Assert.Equal("Contato não encontrado", excecao.Message);
    }

    [Fact]
    public async Task DeleteAsync_QuandoContatoExiste_DeveDeletar()
    {
        var id = Guid.NewGuid();
        var contato = new Contact { Id = id, Name = "João", Email = "joao@email.com", Phone = "81999990000" };

        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(contato);
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);

        await _service.DeleteAsync(id);

        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_QuandoContatoNaoExiste_DeveLancarExcecao()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Contact?)null);

        var excecao = await Assert.ThrowsAsync<Exception>(
            () => _service.DeleteAsync(Guid.NewGuid())
        );
        Assert.Equal("Contato não encontrado", excecao.Message);
    }
}