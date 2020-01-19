using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Interfaces.Models;
using Contracts.Interfaces.Repositories;
using Contracts.Models;

namespace Data.Dapper.Repositories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly BaseDataAcсess _baseDataAcсess;
        private const string GetGroupClassesQuery = @"select
											cl.id as ClassId,
											cl.lesson_name as LessonName,
											cl.room_name as RoomName,
											cl.group_id as GroupId,
											cl.time as ClassTime,
											cl.access as ClassAccess,
											cl.class_type_name as ClassTypeName,
											cl.term_id as ClassTermId,
											cl.teacher_id as ClassTeacherId,
											cl.duration as ClassDuration,
											ct.description as ClassTypeDescription,
											r.description as RoomDescrprion,
											t.start_date as TermStartDate,
											t.end_date as TermEndDate,
											t.description as TermDescription,
											l.description as LessonDescription,
											g.name as GroupName,
											g.access as GroupAccess,
											g.invite_tag as GroupInviteTag,
											g.description as GroupDescription,
											u.name as TeacherName,
											u.surname as TeacherSurname,
											u.patronymic as TeacherPatronymic,
											u.phone as TeacherPhone,
											u.email as TeacherEmail
											from classes cl
										        left join rooms r on cl.room_name = r.name
												left join terms t on cl.term_id = t.id
												left join class_types ct on cl.class_type_name = ct.name
												left join lessons l on cl.lesson_name = l.name
												left join groups g on cl.group_id = g.id
												left join users u on cl.teacher_id = u.id ";

        public ClassesRepository(BaseDataAcсess baseDataAcсess)
        {
            _baseDataAcсess = baseDataAcсess;
        }

        public async Task<IClass> GetById(int id)
        {
	        if (id == 0)
	        {
		        return null;
	        }
	        var byIdQuery = GetGroupClassesQuery + $"where cl.id = {id};";

	        var dbResult = await _baseDataAcсess.ExecuteQueryWhichReturnsCollectionAsync<DbModel>(byIdQuery);
	        return Parse(dbResult?.FirstOrDefault());
        }

        public async Task<ICollection<IClass>> GetGroupClasses(int groupId)
        {
	        var result = new Collection<IClass>();
	        if (groupId == 0)
	        {
		        return null;
	        }
	        var byGroupQuery = GetGroupClassesQuery + $"where g.id = {groupId};";

	        var dbResult = await _baseDataAcсess.ExecuteQueryWhichReturnsCollectionAsync<DbModel>(byGroupQuery);
	        foreach (var item in dbResult)
	        {
		        result.Add(Parse(item));
	        }

	        return result;
        }

        public async Task<ICollection<IClass>> GetClassesByTerm(int termId)
        {
	        var result = new Collection<IClass>();
	        if (termId == 0)
	        {
		        return null;
	        }
	        var byTermQuery = GetGroupClassesQuery + $"where t.id = {termId};";

	        var dbResult = await _baseDataAcсess.ExecuteQueryWhichReturnsCollectionAsync<DbModel>(byTermQuery);
	        foreach (var item in dbResult)
	        {
		        result.Add(Parse(item));
	        }

	        return result;
        }

        public async Task<ICollection<IClass>> GetGroupClassesByTerm(int groupId, int termId)
        {
	        var result = new Collection<IClass>();
	        if (termId == 0)
	        {
		        return null;
	        }
	        var byGroupAndTermQuery = GetGroupClassesQuery + $"where t.id = {termId} and g.id = {groupId};";

	        var dbResult = await _baseDataAcсess.ExecuteQueryWhichReturnsCollectionAsync<DbModel>(byGroupAndTermQuery);
	        foreach (var item in dbResult)
	        {
		        result.Add(Parse(item));
	        }

	        return result;
        }

        private static IClass Parse(DbModel dbModel)
        {
	        if (dbModel == null)
	        {
		        return null;
	        }
	        var lesson = new Lesson(dbModel.lessonname, dbModel.lessondescription);
	        var room = new Room(dbModel.roomname, dbModel.roomdescription);
	        var group = new Group(dbModel.groupid, dbModel.groupname, dbModel.groupaccess, dbModel.groupinvitetag,
		        dbModel.groupdescription, null);
	        var classType = new ClassType(dbModel.classtypename, dbModel.classtypedescription);
	        var term = new Term(dbModel.classtermid, dbModel.termstartdate, dbModel.termenddate, dbModel.termdescription);
	        var teacher = new User(dbModel.classteacherid, dbModel.teachername, dbModel.teachersurname,
		        dbModel.teacherpatronymic, dbModel.teacherphone, dbModel.teacheremail, null, null);
	        return new Class(dbModel.classid, dbModel.classtime, dbModel.classaccess, dbModel.classduration, lesson, room,
		        group, classType, term, teacher);
        }
    }
}