#  AgendaCRUD

CRUD de agenda de contatos desenvolvido como teste técnico para processo seletivo de estágio.

##  Tecnologias

| Camada     | Tecnologia                   |
|------------|------------------------------|
| Backend    | ASP.NET Core (C#)            |
| Frontend   | Vue.js                       |
| Banco      | SQL Server                   |
| Container  | Docker + Docker Compose      |

---

##  Estrutura do Projeto

```
meuCRUD/
├── AgendaCrud_1/        # Backend ASP.NET Core
├── agenda-front/        # Frontend Vue.js
├── AgendaCrud_1.Dockerfile
├── agenda-front.Dockerfile
├── docker-compose.yml
└── README.md
```

---

##  Como Rodar

###  Opção 1 — Docker (recomendado)

> Pré-requisito: [Docker](https://www.docker.com/) instalado.

```bash
# Clone o repositório
git clone https://github.com/DiogoBarros26/meuCRUD.git
cd meuCRUD

# Suba todos os serviços
docker compose up --build
```

Após subir, acesse:

- **Frontend:** http://localhost:8080
- **Backend (API):** http://localhost:5000

---

### Opção 2 — Rodando manualmente (sem Docker)

#### Backend

```bash
cd AgendaCrud_1
dotnet restore
dotnet run
```

> A API ficará disponível em `http://localhost:5000`

#### Frontend

```bash
cd agenda-front
npm install
npm run serve
```

> O frontend ficará disponível em `http://localhost:8081`

---

##  Configuração do Banco de Dados

No modo manual, configure a connection string no arquivo `AgendaCrud_1/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AgendaDB;User Id=sa;Password=SuaSenha;TrustServerCertificate=True;"
  }
}
```

---

##  Funcionalidades

- [x] Listar contatos
- [x] Cadastrar novo contato
- [x] Editar contato existente
- [x] Excluir contato

---

##  Autor

Desenvolvido por **Diogo Barros**
