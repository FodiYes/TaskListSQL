using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskList.Models;
using TaskList.Services;

namespace TaskList.ViewModels
{
    public partial class TaskViewModel : ObservableObject
    {
        private readonly TodoTask _task;
        private readonly DispatcherTimer _timer;
        private readonly TaskService _taskService;
        private bool _isTimerRunning;

        public TaskViewModel(TodoTask task, TaskService taskService)
        {
            _task = task;
            _taskService = taskService;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            StartTimerCommand = new RelayCommand(StartTimer, () => !IsTimerRunning);
            StopTimerCommand = new RelayCommand(StopTimer, () => IsTimerRunning);

            PropertyChanged += TaskViewModel_PropertyChanged;
        }

        private async void TaskViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ElapsedTimeFormatted))
            {
                await _taskService.UpdateTaskAsync(_task);
            }
        }

        public string Name
        {
            get => _task.Name;
            set
            {
                if (_task.Name != value)
                {
                    _task.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _task.Description;
            set
            {
                if (_task.Description != value)
                {
                    _task.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public TaskPriority Priority
        {
            get => _task.Priority;
            set
            {
                if (_task.Priority != value)
                {
                    _task.Priority = value;
                    OnPropertyChanged();
                }
            }
        }

        public TodoTaskStatus Status
        {
            get => _task.Status;
            set
            {
                if (_task.Status != value)
                {
                    _task.Status = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? DueDate
        {
            get => _task.DueDate;
            set
            {
                if (_task.DueDate != value)
                {
                    _task.DueDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan ElapsedTime
        {
            get => _task.ElapsedTime;
            private set
            {
                if (_task.ElapsedTime != value)
                {
                    _task.ElapsedTime = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ElapsedTimeFormatted));
                }
            }
        }

        public string ElapsedTimeFormatted => $"{(int)ElapsedTime.TotalHours:D2}:{ElapsedTime.Minutes:D2}:{ElapsedTime.Seconds:D2}";

        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            private set
            {
                if (_isTimerRunning != value)
                {
                    _isTimerRunning = value;
                    OnPropertyChanged();
                    ((RelayCommand)StartTimerCommand).NotifyCanExecuteChanged();
                    ((RelayCommand)StopTimerCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }

        private void StartTimer()
        {
            _timer.Start();
            IsTimerRunning = true;
        }

        private void StopTimer()
        {
            _timer.Stop();
            IsTimerRunning = false;
            _task.TotalTime = _task.TotalTime.Add(_task.ElapsedTime);
            _taskService.UpdateTaskAsync(_task);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            ElapsedTime = ElapsedTime.Add(TimeSpan.FromSeconds(1));
        }
    }
}
