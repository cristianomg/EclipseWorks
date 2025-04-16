# Task Management API

## 🚀 Projeto

API RESTful para gerenciamento de projetos e tarefas, desenvolvida como parte de um desafio técnico. A solução contempla regras de negócio específicas, controle de histórico de alterações, e relatórios de desempenho.

---

## 📚 Funcionalidades Implementadas (Fase 1)

- Listagem de projetos por usuário
- Visualização de tarefas de um projeto específico
- Criação de novos projetos
- Criação de tarefas dentro de um projeto
- Atualização de status ou detalhes da tarefa
- Remoção de tarefas
- Regras de negócio implementadas:
  - Prioridade da tarefa imutável após criação
  - Validação para não excluir projetos de outro usuário
  - Validação para não excluir projetos com tarefas pendentes
  - Registro de histórico de atualizações de tarefas (campo, valor antigo, valor novo, data, usuário)
  - Limite de até 20 tarefas por projeto
  - Comentários em tarefas que também são registrados no histórico
  - Relatório de desempenho de tarefas concluídas nos últimos 30 dias (acesso restrito a usuários com perfil de gerente)

---

## 💼 Requisitos para execução

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Docker
- Banco de dados: PostgreSQL / SQL Server

### 📦 Rodando com Docker

```bash
docker-compose up --build
```

A aplicação estará disponível em: `http://localhost:8080/swagger/index.html`

---

## ✏️ Fase 2: Refinamento (perguntas para o PO)

1. Existe uma ordem de transição (ex: "Em andamento" → "Concluído")?
2. Quais campos podem ser alterados em uma tarefa após sua criação? E quais são imutáveis além da prioridade?
3. Uma tarefa concluída pode ser reaberta ou modificada?
4. Existe prazo para conclusão das tarefas. Como a aplicação deve se comportar em caso de vencimento?
5. Os relatórios de desempenho devem considerar tarefas reabertas ou apenas concluídas pela primeira vez?
6. Devemos emitir alertas automáticos (ex: por e-mail ou notificacão) quando tarefas estiverem perto do vencimento?
7. Haverá necessidade de anexar arquivos ou imagens nas tarefas ou comentários futuramente?

---

## 🔄 Fase 3: Arquitetura e Futuras Melhorias

### Arquitetura & Projeto

- Implementação do padrão **Domain-Driven Design (DDD)** para separar claramente as camadas da aplicação.
- Uso de **CQRS com MediatR** para separação clara entre comandos e queries.
- Uso de **Script automático de migração** acoplado à aplicação (via `DbContext.Database.Migrate()`).

---

### Futuras Melhorias
- Incluir **log centralizado** com Serilog + Sink para arquivos e/ou Elastic.
- Expor **métricas com Prometheus/Grafana**.
- Notificação por e-mail ou push para tarefas prestes a vencer.
- Relatórios adicionais:
  - Tarefas atrasadas por usuário
  - Tempo médio de conclusão
  - Projetos com maior taxa de atraso
- Implementação de **soft delete** para manter histórico.
- Internacionalização (i18n) das mensagens de erro e sucesso.
- Incluir **mapeamento** com Automapper para trafegar apenas dados necessários.

---
