﻿using TaskManagement.DataAccessLayer.Common;
using TaskManagement.DataAccessLayer.DataModels;
using TaskManagement.DataAccessLayer.Repository.RepositoryParameters;

namespace TaskManagement.DataAccessLayer.Repository.AstractClass
{
    public interface ITaskRepository
    {
        Task<PagedList<TaskItem>> GetAllTaskAsyn(TaskRP taskRP, bool trackChanges);

        void CreateTaskAsyn(TaskItem taskItem);

        Task<TaskItem> GetTaskByTitleAsyn(string title, bool trackChanges);

        Task<TaskItem> GetTaskByIdAsyn(Guid id, bool trackChanges);

        void UpdateTaskAsyn(TaskItem taskItem);

        void DeleteTaskAsyn(TaskItem taskItem);
    }
}
