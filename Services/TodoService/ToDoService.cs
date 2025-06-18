using System.Text.Json;
using TodoApp.Models;


namespace TodoApp.Services
{
  // The TodoService class handles all operations related to loading,
  // saving, and managing the list of to-do items.
  public class TodoService
  {
    // Load and save todoData.json
    // Methods: AddTodo, GetAll, MarkAsComplete, DeleteTodo

    private readonly string _filePath = "Data/todoData.json";

    // In-memory list to store all to-do items
    private List<TodoItem> _todoItemList;

    // Constructor: runs once when a new TodoService is created
    public TodoService()
    {
      // Create the "Data" folder if it doesn't exist
      if (!Directory.Exists("Data"))
      {
        Directory.CreateDirectory("Data");
      }

      // If the data file exists, read it and load the items
      if (File.Exists(_filePath))
      {
        string content = File.ReadAllText(_filePath);  // Read JSON text from file
        _todoItemList = JsonSerializer.Deserialize<List<TodoItem>>(content) ?? new List<TodoItem>();   // Deserialize into list or use empty list
      }
      else
      {
        // If file doesn't exist, start with an empty list and create the file
        _todoItemList = new List<TodoItem>();
        SaveChanges();
      }
    }

    // SaveChanges: writes the current list to the JSON file
    private void SaveChanges()
    {
      // Convert the list of TodoItem objects to a JSON string
      string todoItemJson = System.Text.Json.JsonSerializer.Serialize(_todoItemList);
       // Write the JSON string to the file at the specified path
      File.WriteAllText(_filePath, todoItemJson);
    }

    // AddItems: adds a new to-do item to the list
    public void AddItems(TodoItem item)
    {
      // Automatically assign a unique ID: max ID + 1, or 1 if list is empty
      item.Id = _todoItemList.Any() ? _todoItemList.Max(x => x.Id) + 1 : 1;
      // Add the item to the in-memory list
      _todoItemList.Add(item);
      // Save the updated list to the file
      SaveChanges();
    }

    // GetAll: returns the full list of to-do items
    public List<TodoItem> GetAll() => _todoItemList;

    // removeItems: removes any items with the matching ID from the list
    public void RemoveItems(int id)
    {
      // Remove all items from the list where the ID matches
      _todoItemList.RemoveAll(x => x.Id == id);
      // Save the updated list
      SaveChanges();
    }

    // MarkAsComplete: marks a specific item as completed
    public void MarkAsComplete(int id)
    {
      // Find the first item with the matching ID (or null if not found)
      TodoItem item = _todoItemList.FirstOrDefault(x => x.Id == id);
      // If the item exists, mark it as complete and save
      if (item != null)
      {
        item.IsCompleted = true;
        SaveChanges();
      }
    }
  }
}