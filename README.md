# TaskList 📝

<div align="center">

![TaskList Banner](screenshot.png)

[![.NET](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)](https://www.microsoft.com/windows)
[![SQLite](https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white)](https://www.sqlite.org/index.html)
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)](https://visualstudio.microsoft.com/)

</div>

## 🌟 Overview

TaskList is a sophisticated task management application that combines the sleek aesthetics of Discord with powerful productivity features. Built using modern WPF and .NET 8 technologies, it offers a seamless and intuitive experience for managing your daily tasks and projects.

### 🎯 Key Features

<table>
<tr>
<td width="50%">

#### 📊 Task Management
- Priority levels (Low, Normal, High, Urgent)
- Status tracking (Not Started, In Progress, Completed)
- Rich text descriptions
- Due date scheduling
- Time tracking with start/stop functionality
- Total time tracking per task

</td>
<td width="50%">

#### ⚡ Core Functionality
- Real-time task updates
- Automatic data persistence
- Task time tracking
- SQLite database storage
- Keyboard shortcuts
- Task history tracking

</td>
</tr>
</table>

### 🎨 User Interface

<table>
<tr>
<td width="50%">

#### 💫 Modern Design
- Discord-inspired dark theme
- Smooth animations
- Custom-styled controls
- Responsive layout
- Intuitive navigation
- Time tracking controls

</td>
<td width="50%">

#### 🛠️ Technical Features
- MVVM architecture
- Entity Framework Core
- SQLite database
- Time tracking service
- Automatic state persistence
- Task history logging

</td>
</tr>
</table>

## 📦 Installation

### Prerequisites

Make sure you have the following installed:
- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) with:
  - .NET Desktop Development workload
  - Windows SDK

### Quick Start

1. Clone the repository:
```bash
git clone https://github.com/FodiYes/TaskListSQL.git
cd TaskListSQL
```

2. Build and run:
```bash
dotnet restore
dotnet build
dotnet run --project TaskList
```

Or simply open `TaskList.sln` in Visual Studio and press F5.

## 💡 Usage Guide

### Task Management

#### Creating Tasks
1. Click the "Add Task" button or press `Ctrl+N`
2. Enter task details:
   - Name (required)
   - Description
   - Priority level
   - Due date (optional)

#### Time Tracking
- ▶️ Start timer: Click play button
- ⏹️ Stop timer: Click stop button
- 📊 View total time: Check task details
- 📝 Time history: Automatically logged

#### Task Organization
- Sort by priority or due date
- Filter by status
- Priority levels:
  - 🔴 Urgent
  - 🟡 High
  - 🟢 Normal
  - 🔵 Low

### Keyboard Shortcuts

| Action | Shortcut |
|--------|----------|
| New Task | `Ctrl+N` |
| Save Changes | `Ctrl+S` |
| Delete Task | `Delete` |
| Start Timer | Click play |
| Stop Timer | Click stop |

## 🛠️ Technical Details

### Architecture

The application follows the MVVM pattern with these key components:

```
TaskList/
├── Models/             # Data models and entities
│   ├── TodoTask.cs    # Task entity with time tracking
│   └── TimeTracking.cs # Time tracking history
├── ViewModels/         # MVVM view models
├── Views/              # WPF views
├── Services/           # Business logic
│   ├── TaskService.cs  # Task management
│   └── DatabaseContext.cs # Data persistence
├── Converters/         # Value converters
└── Helpers/           # Utilities
```

### Dependencies

- **UI Framework**
  - WPF (Windows Presentation Foundation)
  - CommunityToolkit.Mvvm

- **Data Access**
  - Microsoft.EntityFrameworkCore.Sqlite
  - Microsoft.EntityFrameworkCore.Tools

## 🤝 Contributing

We welcome contributions! Here's how you can help:

1. 🍴 Fork the repository
2. 🌿 Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. 💾 Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. 📤 Push to the branch (`git push origin feature/AmazingFeature`)
5. 🔄 Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Inspired by Discord's modern UI/UX design
- Built with .NET 8 and WPF
- Uses Entity Framework Core with SQLite
- Time tracking functionality
- Community feedback and contributions

---

<div align="center">

Made with ❤️ by [DAN](https://github.com/FodiYes)

</div>
