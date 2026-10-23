
using SharedTaskBoard.Models;

namespace SharedTaskBoard.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();

    public event Action? OnChange;

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(task => task.Id == id);
    }

    public void Add(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return;

        _tasks.Add(new TaskItem
        {
            Title = title.Trim(),
            CreatedAt = DateTime.Now
        });

        NotifyStateChanged();
    }

    public void Toggle(Guid id)
    {
        var task = GetById(id);

        if (task is null)
            return;

        task.IsCompleted = !task.IsCompleted;
        NotifyStateChanged();
    }

    public void Delete(Guid id)
    {
        var task = GetById(id);

        if (task is null)
            return;

        _tasks.Remove(task);
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
