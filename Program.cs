using System.Diagnostics;
using TodoApp.Models;
using TodoApp.Services;


namespace TodoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
             // Create a new instance of the TodoService to manage to-do items
            TodoService service = new TodoService();

            // Infinite loop to keep showing the menu until the user exits
            while (true)
            {
                // Display the menu options
                Console.WriteLine("\n==To-Do List Menu ==");
                Console.WriteLine("1. View All Tasks");
                Console.WriteLine("2. Add New Task");
                Console.WriteLine("3. Mark Task as Complete");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                // Ask user for input
                Console.WriteLine("Choose an option: ");
                string? input = Console.ReadLine();

                // Handle the user's menu selection
                switch (input)
                {
                    case "1":
                        // View all tasks
                        List<TodoItem> tasks = service.GetAll();
                        Console.WriteLine("\n ---All Tasks ---");
                        // Loop through each task and display it
                        foreach (TodoItem task in tasks)
                        {
                            // Display format: [ID] [X]/[ ] Title (Due: Date, Priority: #)
                            Console.WriteLine($"[{task.Id}] {(task.IsCompleted ? "[X]" : "[ ]")} {task.Title} (Due: {task.DueDate.ToShortDateString()}, Priority: {task.Priority})");
                        }
                        break;

                    case "2":
                        // Add a new task
                        Console.Write("Title: ");
                        string title = Console.ReadLine()!;
                        Console.Write("Description: ");
                        string description = Console.ReadLine()!;
                        Console.Write("DueDate (yyyy-mm-dd): ");
                        DateTime duedate = DateTime.Parse(Console.ReadLine()!);
                        Console.Write("Priority (1 = High, 2 = Medium, 3 = Low):");
                        int prio = int.Parse(Console.ReadLine()!);
                        
                        // Create a new task object with the entered values
                        TodoItem item = new TodoItem
                        {
                            Title = title,
                            Description = description,
                            DueDate = duedate,
                            IsCompleted = false,
                            Priority = prio
                        };

                        // Add the new task to the list
                        service.AddItems(item);
                        Console.WriteLine("Task added!");
                        break;

                    case "3":
                        // Mark a task as completed
                        // Ask for the ID of the task to mark as complete
                        Console.WriteLine("Enter task ID to complete: ");
                        int completeID = int.Parse(Console.ReadLine()!);
                        // Mark the task as completed
                        service.MarkAsComplete(completeID);
                        Console.WriteLine("Marked as complete!");
                        break;

                    case "4":
                        // Delete a task
                        // Ask for the ID of the task to delete
                        Console.WriteLine("Enter task ID to delete: ");
                        int deletedId = int.Parse(Console.ReadLine()!);
                        // Remove the task with the given ID
                        service.RemoveItems(deletedId);
                        Console.WriteLine("Deleted!");
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
