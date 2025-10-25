using TaskTracker.CLI.Data;
using TaskTracker.CLI.Services;
using TaskStatus = TaskTracker.CLI.Models.TaskStatus;

var repo = new TaskRepository();
var service = new TaskService(repo);

if (args.Length == 0 || args[0].Equals("help", StringComparison.CurrentCultureIgnoreCase))
{
    foreach (var line in service.GetHelpCommands())
    {
        Console.WriteLine(line);
    }
    return;
}

var command = args[0].ToLower();

try
{
    switch (command)
    {
        case "add":
            EnsureArguments(args, 2, "add <description>");
            var task = service.AddTask(args[1]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Task added successfully: '{task.Description}' (ID: {task.Id})");
            break;
        case "update":
            var task2 = service.UpdateTaskDescription(int.Parse(args[1]), args[2]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Task 'ID: {task2.Id}' updated to: '{task2.Description}'");
            break;
        case "delete":
            service.DeleteTask(int.Parse(args[1]));
            Console.WriteLine($"Task 'ID: {args[1]}' removed successfully.");
            break;
        case "mark-in-progress":
            service.UpdateTaskStatus(int.Parse(args[1]), TaskStatus.InProgress);
            break;
        case "mark-done":
            service.UpdateTaskStatus(int.Parse(args[1]), TaskStatus.Done);
            break;
        case "list":
            service.PrintAllTasks();
            break;
        case "list done":
            service.PrintTasksByStatus(TaskStatus.Done);
            break;
        case "list todo":
            service.PrintTasksByStatus(TaskStatus.ToDo);
            break;
        case "list in-progress":
            service.PrintTasksByStatus(TaskStatus.InProgress);
            break;
        case "clear":
            Console.WriteLine("Cleaning all tasks...");
            service.ClearAllTasks();
            break;
        case "print":
            service.PrintTaskById(int.Parse(args[1]));
            break;

        default:
            Console.WriteLine("Wrong command. Type 'help' to see all the commands.");
            break;
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {ex.Message}");
    Console.ResetColor();
    throw;
}

return;

void EnsureArguments(string[] args, int expected, string usage)
{
    if (args.Length >= expected) return;
    Console.WriteLine($"Wrong use. Exemple: {usage}");
    Environment.Exit(1);
}