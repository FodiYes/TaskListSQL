using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskList.Models;
using TaskList.Services;

namespace TaskList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ObservableCollection<TodoTask> _allTasks;
        private readonly ObservableCollection<TodoTask> _filteredTasks;
        private TodoTask? _selectedTask;
        private TaskViewModel? _selectedTaskViewModel;
        private string _searchText = string.Empty;
        private readonly TaskService _taskService;

        public MainViewModel()
        {
            _taskService = new TaskService();
            _allTasks = new ObservableCollection<TodoTask>();
            _filteredTasks = new ObservableCollection<TodoTask>();
            
            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, () => SelectedTask != null);
            
            // Load tasks from database
            LoadTasks();
        }

        private async void LoadTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            _allTasks.Clear();
            foreach (var task in tasks)
            {
                _allTasks.Add(task);
            }
            FilterTasks();
        }

        public ObservableCollection<TodoTask> Tasks => _filteredTasks;

        public TodoTask? SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (SetProperty(ref _selectedTask, value))
                {
                    SelectedTaskViewModel = value != null ? new TaskViewModel(value, _taskService) : null;
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

        private async void AddTask()
        {
            await AddTask("New Task", "Enter description", TaskPriority.Normal);
        }

        private async Task AddTask(string name, string description, TaskPriority priority)
        {
            var newTask = new TodoTask
            {
                Name = name,
                Description = description,
                Priority = priority,
                Status = TodoTaskStatus.NotStarted,
                DueDate = DateTime.Now.AddDays(7)
            };

            await _taskService.AddTaskAsync(newTask);
            _allTasks.Add(newTask);
            FilterTasks();
            SelectedTask = newTask;
        }

        private async void DeleteTask()
        {
            if (SelectedTask != null)
            {
                await _taskService.DeleteTaskAsync(SelectedTask);
                _allTasks.Remove(SelectedTask);
                FilterTasks();
                SelectedTask = null;
            }
        }

        private void FilterTasks()
        {
            _filteredTasks.Clear();
            var filteredTasks = string.IsNullOrWhiteSpace(SearchText)
                ? _allTasks
                : _allTasks.Where(t => t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                     t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            foreach (var task in filteredTasks)
            {
                _filteredTasks.Add(task);
            }
        }
    }
}
