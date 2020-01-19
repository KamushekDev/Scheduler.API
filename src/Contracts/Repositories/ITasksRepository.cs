using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface ITasksRepository
    {
        public Task<ITask> GetById(int id);

        public Task<ICollection<ITask>> GetByLesson(ILesson lesson)
            => GetByLesson(lesson.Name);

        public Task<ICollection<ITask>> GetByLesson(string lessonName);

        /// <summary>
        /// Вернуть все задания, время сдачи которых ПОСЛЕ дедлайна
        /// </summary>
        /// <param name="deadline">Время дедлайна</param>
        /// <returns></returns>
        public Task<ICollection<ITask>> GetLessonsWithActiveDeadLine(DateTime deadline);

        public Task<ICollection<ITask>> GetLessonsByUser(IUser user)
            => GetLessonsByUser(user.Id);

        public Task<ICollection<ITask>> GetLessonsByUser(int userId);

        public Task<ICollection<ITask>> GetLessonsByGroup(IGroup group)
            => GetLessonsByGroup(group.Id);

        public Task<ICollection<ITask>> GetLessonsByGroup(int groupId);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}