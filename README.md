# Sistema de Agendamento de Salas

Aplicação desktop desenvolvida em **C# com Windows Forms (.NET 10)** para gerenciamento de salas e agendamentos. Utiliza **PostgreSQL** como banco de dados, com conexão via biblioteca **Npgsql**.

---

## Tecnologias utilizadas

- **Linguagem:** C# (.NET 10)
- **Interface:** Windows Forms (WinForms)
- **Banco de dados:** PostgreSQL
- **Driver de conexão:** Npgsql
- **IDE recomendada:** Visual Studio 2022+

---

## Estrutura do projeto

```
ProjetoAgendamento/
├── Database/
│   └── Conexao.cs                       # Gerencia a conexão com o PostgreSQL
├── Forms/
│   ├── FormMenu.cs / .Designer.cs        # Tela inicial com menu de navegação
│   ├── FormSala.cs / .Designer.cs        # Cadastro e gerenciamento de salas
│   └── FormAgendamento.cs / .Designer.cs # Cadastro e gerenciamento de agendamentos
├── Models/
│   ├── Sala.cs                           # Modelo da entidade Sala
│   └── Agendamento.cs                    # Modelo da entidade Agendamento
├── Program.cs                            # Ponto de entrada da aplicação
└── script.sql                            # Script de criação do banco de dados
```

---

## Banco de dados

Execute o arquivo `script.sql` no seu PostgreSQL para criar as tabelas necessárias. O esquema contém duas tabelas:

- **sala** — armazena as salas cadastradas (`id`, `nome`)
- **agendamento** — armazena os agendamentos (`id`, `id_sala`, `data_inicio`, `data_fim`)

A string de conexão está configurada em `Database/Conexao.cs`. Ajuste os parâmetros de host, porta, usuário, senha e nome do banco conforme seu ambiente:

```csharp
"Host=localhost;Port=5432;Database=nome_do_banco;Username=seu_usuario;Password=sua_senha"
```

---

## Funcionalidades

### Gerenciar Salas
- Cadastrar nova sala informando o nome
- Editar nome de uma sala existente (selecione na lista e digite o novo nome)
- Excluir sala (com confirmação)
- Listagem de todas as salas cadastradas

### Gerenciar Agendamentos
- Criar agendamento selecionando sala, data/hora de início e data/hora de fim
- Editar agendamento existente (selecione na lista e altere os campos)
- Excluir agendamento (com confirmação)
- Listagem de todos os agendamentos com nome da sala, início e fim

---

## Fluxo de navegação

```
Aplicação inicia
      ↓
  FormMenu (Menu Principal)
  ├── [Gerenciar Salas]         → FormSala        → [← Voltar ao Menu]
  └── [Gerenciar Agendamentos]  → FormAgendamento → [← Voltar ao Menu]
```

> **Importante:** cadastre ao menos uma sala antes de criar agendamentos, pois o formulário de agendamento exige uma sala vinculada.

---

## Como executar

1. Clone o repositório
2. Instale o PostgreSQL e execute o `script.sql`
3. Configure a string de conexão em `Database/Conexao.cs`
4. Abra a solução `ProjetoAgendamento.slnx` no Visual Studio
5. Compile e execute (`F5`)
