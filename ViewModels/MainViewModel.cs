using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _taskTitle = string.Empty;

        [ObservableProperty]
        private int _count;

        public ObservableCollection<ToDoTask> Tasks { get; } = new();

        [RelayCommand]
        private void CreateTask()
        {
            if (string.IsNullOrWhiteSpace(TaskTitle) || TaskTitle.Length < 3)
                return;

            var newTask = new ToDoTask
            {
                //Id = Tasks.Count,
                Title = TaskTitle
            };

            Tasks.Add(newTask);
            Count = Tasks.Count;
            TaskTitle = string.Empty;
        }

        [RelayCommand]
        private void EditTask(ToDoTask task)
        {
            task.EditedText = task.Title;
            task.IsEditing = true;
            task.IsDisplaying = false;
        }

        [RelayCommand]
        private void SaveTask(ToDoTask task)
        {
            if (!string.IsNullOrWhiteSpace(task.EditedText))
            {
                task.Title = task.EditedText;
            }
            task.IsEditing = false;
            task.IsDisplaying = true;
            task.EditedText = string.Empty;
        }

        [RelayCommand]
        private void CancelEdit(ToDoTask task)
        {
            task.IsEditing = false;
            task.IsDisplaying = true;
            task.EditedText = string.Empty;
        }

        [RelayCommand]
        private void DeleteTask(ToDoTask task)
        {
            Tasks.Remove(task);
            Count = Tasks.Count;
        }
    }
}