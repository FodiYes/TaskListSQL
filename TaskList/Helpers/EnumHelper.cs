using System;
using System.Collections.Generic;
using TaskList.Models;

namespace TaskList.Helpers
{
    public static class EnumHelper
    {
        public static Array TaskPriorityValues => Enum.GetValues(typeof(TaskPriority));
        public static Array TaskStatusValues => Enum.GetValues(typeof(TodoTaskStatus));
    }
}
