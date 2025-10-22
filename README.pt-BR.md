[English](./README.md) | [Português](./README.pt-BR.md)
# TaskTracker

O **TaskTracker** é uma aplicação de linha de comando (CLI) feita em C# que permite criar e gerenciar tarefas. Ele é pensado para uso simples via terminal: você pode adicionar, atualizar, deletar e listar tarefas, além de marcar o status (“To Do”, “In Progress” ou “Done”).

O projeto utiliza um **TaskRepository** para persistir dados e um **TaskService** que encapsula a lógica de negócio.

## Funcionalidades

* **Adicionar tarefa** – cria uma nova tarefa com descrição definida pelo usuário.
* **Atualizar descrição** – altera a descrição de uma tarefa existente usando seu ID.
* **Deletar tarefa** – remove definitivamente uma tarefa.
* **Alterar status** – marca a tarefa como “in‑progress” ou “done”.
* **Listar tarefas** – exibe todas as tarefas ou filtra por status (ex.: apenas tarefas concluídas).
* **Imprimir tarefa** – mostra detalhes de uma tarefa específica pelo ID.
* **Limpar todas** – apaga todas as tarefas (útil para começar do zero).
* **Ajuda** – mostra a lista de comandos disponíveis e exemplos de uso.

Essas ações são implementadas no arquivo `Program.cs`, que interpreta o comando passado e chama os métodos apropriados do serviço.

## Pré‑requisitos

* [.NET 6+ SDK](https://dotnet.microsoft.com/download) instalado.

## Instalação e execução

1. Clone o repositório:

   ```bash
   git clone https://github.com/arturvas/TaskTracker.git
   cd TaskTracker
   ```

2. Compile e execute o projeto CLI usando o comando `dotnet run` dentro da pasta `TaskTracker.CLI`:

   ```bash
   cd TaskTracker.CLI
   dotnet run -- [comando] [argumentos...]
   ```

   > **Observação:** o argumento `--` separa os argumentos da CLI do `dotnet`.

## Exemplos de uso

* **Adicionar uma tarefa**

  ```bash
  dotnet run -- add "Comprar leite"
  ```

* **Atualizar descrição**

  ```bash
  dotnet run -- update 1 "Comprar leite e pão"
  ```

* **Marcar tarefa como em andamento**

  ```bash
  dotnet run -- mark-in-progress 1
  ```

* **Marcar tarefa como concluída**

  ```bash
  dotnet run -- mark-done 1
  ```

* **Listar todas as tarefas**

  ```bash
  dotnet run -- list
  ```

* **Filtrar por status**

  ```bash
  dotnet run -- "list done"       # apenas concluídas
  dotnet run -- "list todo"       # pendentes
  dotnet run -- "list in-progress" # em andamento
  ```

* **Imprimir tarefa específica**

  ```bash
  dotnet run -- print 1
  ```

* **Limpar todas as tarefas**

  ```bash
  dotnet run -- clear
  ```

* **Ajuda**

  ```bash
  dotnet run -- help
  ```

## Estrutura do projeto

* **TaskTracker.CLI** – Projeto principal da interface de linha de comando (CLI).
* **Data** – Persistência de tarefas (ex.: em JSON).
* **Models** – Modelos de dados (TaskItem, status etc.).
* **Services** – Lógica de negócios, manipulando tarefas e delegando para o repositório.
* **Interfaces** – Interfaces para serviços e repositórios.
* **Tests** – Suite de testes que cobre serviços e repositórios.