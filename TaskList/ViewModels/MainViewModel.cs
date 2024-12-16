using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskList.Models;

namespace TaskList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ObservableCollection<TodoTask> _allTasks;
        private readonly ObservableCollection<TodoTask> _filteredTasks;
        private TodoTask? _selectedTask;
        private TaskViewModel? _selectedTaskViewModel;
        private string _searchText = string.Empty;

        public MainViewModel()
        {
            _allTasks = new ObservableCollection<TodoTask>();
            _filteredTasks = new ObservableCollection<TodoTask>();
            
            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, () => SelectedTask != null);
            
            // Add some sample tasks
            AddTask("Complete project documentation", "Write comprehensive documentation for the project", TaskPriority.High);
            AddTask("Fix bugs", "Address reported issues in the bug tracker", TaskPriority.Urgent);
            AddTask("Update dependencies", "Update all project dependencies to their latest versions", TaskPriority.Normal);
        }

        public ObservableCollection<TodoTask> Tasks => _filteredTasks;

        public TodoTask? SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (SetProperty(ref _selectedTask, value))
                {
                    SelectedTaskViewModel = value != null ? new TaskViewModel(value) : null;
                    ((RelayCommand)DeleteTaskCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public TaskViewModel? SelectedTaskViewModel
        {
            get => _selectedTaskViewModel;
            private set => SetProperty(ref _selectedTaskViewModel, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterTasks();
                }
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        private void AddTask()
        {
            AddTask("New Task", "Enter description", TaskPriority.Normal);
        }

        private void AddTask(string name, string description, TaskPriority priority)
        {
            var newTask = new TodoTask
            {
                Name = name,
                Description = description,
                Priority = priority,
                Status = TodoTaskStatus.NotStarted,
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7)
            };

            _allTasks.Add(newTask);
            FilterTasks();
            SelectedTask = newTask;
        }

        private void DeleteTask()
        {
            if (SelectedTask != null)
            {
                _allTasks.Remove(SelectedTask);
                FilterTasks();
                SelectedTask = null;
            }
        }

        private void FilterTasks()
        {
            _filteredTasks.Clear();
            var searchTerm = SearchText.Trim().ToLower();
            
            var filteredTasks = string.IsNullOrEmpty(searchTerm)
                ? _allTasks
                : _allTasks.Where(t =>
                    t.Name.ToLower().Contains(searchTerm) ||
                    t.Description.ToLower().Contains(searchTerm));

            foreach (var task in filteredTasks)
            {
                _filteredTasks.Add(task);
            }
        }
    }
}
