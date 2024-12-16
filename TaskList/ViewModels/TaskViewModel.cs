using System;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskList.Models;

namespace TaskList.ViewModels
{
    public partial class TaskViewModel : ObservableObject
    {
        private readonly TodoTask _task;
        private readonly DispatcherTimer _timer;
        private TimeSpan _elapsedTime;
        private bool _isTimerRunning;
        private DateTime? _currentTrackingStart;

        public TaskViewModel(TodoTask task)
        {
            _task = task;
            _elapsedTime = task.TotalTime;
            _isTimerRunning = false;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            StartTimerCommand = new RelayCommand(StartTimer, () => !IsTimerRunning);
            StopTimerCommand = new RelayCommand(StopTimer, () => IsTimerRunning);
        }

        public string Name
        {
            get => _task.Name;
            set => SetProperty(_task.Name, value, _task, (t, n) => t.Name = n);
        }

        public string Description
        {
            get => _task.Description;
            set => SetProperty(_task.Description, value, _task, (t, d) => t.Description = d);
        }

        public DateTime StartDate
        {
            get => _task.StartDate;
            set => SetProperty(_task.StartDate, value, _task, (t, d) => t.StartDate = d);
        }

        public DateTime DueDate
        {
            get => _task.DueDate;
            set => SetProperty(_task.DueDate, value, _task, (t, d) => t.DueDate = d);
        }

        public TaskPriority Priority
        {
            get => _task.Priority;
            set => SetProperty(_task.Priority, value, _task, (t, p) => t.Priority = p);
        }

        public TodoTaskStatus Status
        {
            get => _task.Status;
            set => SetProperty(_task.Status, value, _task, (t, s) => t.Status = s);
        }

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            private set => SetProperty(ref _elapsedTime, value);
        }

        public string ElapsedTimeFormatted => $"{(int)ElapsedTime.TotalHours}:{ElapsedTime.Minutes:D2}:{ElapsedTime.Seconds:D2}";

        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            private set
            {
                if (SetProperty(ref _isTimerRunning, value))
                {
                    ((RelayCommand)StartTimerCommand).NotifyCanExecuteChanged();
                    ((RelayCommand)StopTimerCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }

        private void StartTimer()
        {
            if (!IsTimerRunning)
            {
                _currentTrackingStart = DateTime.Now;
                _timer.Start();
                IsTimerRunning = true;
                Status = TodoTaskStatus.InProgress;
            }
        }

        private void StopTimer()
        {
            if (IsTimerRunning)
            {
                _timer.Stop();
                IsTimerRunning = false;

                if (_currentTrackingStart.HasValue)
                {
                    var tracking = new TimeTracking
                    {
                        TaskId = _task.Id,
                        StartTime = _currentTrackingStart.Value,
                        EndTime = DateTime.Now,
                        Task = _task
                    };
                    _task.TimeTrackings.Add(tracking);
                    _currentTrackingStart = null;

                    ElapsedTime = _task.TotalTime = _task.CalculateTotalTime();
                }
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_currentTrackingStart.HasValue)
            {
                ElapsedTime = _task.TotalTime + (DateTime.Now - _currentTrackingStart.Value);
                OnPropertyChanged(nameof(ElapsedTimeFormatted));
            }
        }
    }
}
