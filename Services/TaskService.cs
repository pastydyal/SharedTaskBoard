
using SharedTaskBoard.Models;

namespace SharedTaskBoard.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();

    public event Action? OnChange;

    // Возвращает только неудалённые задачи
    public List<TaskItem> GetAll()
    {
        return _tasks
            .Where(task => !task.IsDeleted)
            .ToList();
    }

    // Количество неудалённых задач
    public int GetCount()
    {
        return _tasks.Count(task => !task.IsDeleted);
    }

    // Добавление задачи
    public void Add(TaskItem task)
    {
        _tasks.Add(task);
        NotifyStateChanged();
    }

    // Изменение статуса выполнения
    public void Toggle(Guid id)
    {
        var task = _tasks.FirstOrDefault(task =>
            task.Id == id && !task.IsDeleted);

        if (task is not null)
        {
            task.IsCompleted = !task.IsCompleted;
            NotifyStateChanged();
        }
    }

    // Мягкое удаление задачи
    public void Delete(Guid id)
    {
        var task = _tasks.FirstOrDefault(task =>
            task.Id == id && !task.IsDeleted);

        if (task is not null)
        {
            task.IsDeleted = true;
            NotifyStateChanged();
        }
    }

    // Получение задачи по ID
    public TaskItem? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(task =>
            task.Id == id && !task.IsDeleted);
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
