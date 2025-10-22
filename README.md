[English](./README.md) | [Português](./README.pt-BR.md)

# TaskTracker 
[A roadmap.sh project](https://roadmap.sh/projects/task-tracker)

**TaskTracker** is a command‑line interface (CLI) application written in C# for creating and managing tasks. It aims to provide a simple terminal‑based workflow: you can add, update, delete and list tasks, as well as mark them as “To Do”, “In Progress” or “Done”.

The project uses a **TaskRepository** for data persistence and a **TaskService** that encapsulates the business logic.

## Features

* **Add task** – create a new task with a user‑defined description.
* **Update description** – modify the description of an existing task using its ID.
* **Delete task** – permanently remove a task.
* **Change status** – mark a task as *in‑progress* or *done*.
* **List tasks** – display all tasks or filter by status (e.g., only completed tasks).
* **Print task** – show details of a specific task by its ID.
* **Clear all** – delete all tasks (useful for starting fresh).
* **Help** – print a list of available commands and usage examples.

These actions are implemented in `Program.cs`, which parses the command‑line arguments and calls the appropriate service methods.

## Prerequisites

* [.NET 6+ SDK](https://dotnet.microsoft.com/download) installed.

## Installation and Running

1. Clone the repository:

   ```bash
   git clone https://github.com/arturvas/TaskTracker.git
   cd TaskTracker
   ```

2. Build and run the CLI project using `dotnet run` inside the `TaskTracker.CLI` directory:

   ```bash
   cd TaskTracker.CLI
   dotnet run -- [command] [arguments...]
   ```

   > **Note:** the `--` separates the CLI arguments from `dotnet`.

## Usage Examples

* **Add a task**

  ```bash
  dotnet run -- add "Buy milk"
  ```

* **Update description**

  ```bash
  dotnet run -- update 1 "Buy milk and bread"
  ```

* **Mark a task as in progress**

  ```bash
  dotnet run -- mark-in-progress 1
  ```

* **Mark a task as done**

  ```bash
  dotnet run -- mark-done 1
  ```

* **List all tasks**

  ```bash
  dotnet run -- list
  ```

* **Filter by status**

  ```bash
  dotnet run -- "list done"         # only completed
  dotnet run -- "list todo"         # tasks to do
  dotnet run -- "list in-progress"  # tasks in progress
  ```

* **Print a specific task**

  ```bash
  dotnet run -- print 1
  ```

* **Clear all tasks**

  ```bash
  dotnet run -- clear
  ```

* **Help**

  ```bash
  dotnet run -- help
  ```

## Project Structure

* **TaskTracker.CLI** – main CLI application project.
* **Data** – data persistence (e.g., JSON storage).
* **Models** – data models (TaskItem, status, etc.).
* **Services** – business logic for task handling, delegating to the repository.
* **Interfaces** – interfaces for services and repositories.
* **Tests** – unit tests covering services and repositories.