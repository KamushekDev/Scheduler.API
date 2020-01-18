using System;

namespace Contracts.Models
{
    public class DbModel
    {
        public int taskclassid { get; set; }
        public DateTime taskstarttime { get; set; }
        public DateTime taskendtime { get; set; }
        public string taskname { get; set; }
        public string taskdescription { get; set; }
        public string taskaccess { get; set; }
        public string lessonname { get; set; }
        public string roomname { get; set; }
        public int groupid { get; set; }
        public DateTime classtime { get; set; }
        public string classaccess { get; set; }
        public string classtypename { get; set; }
        public int classtermdatesid { get; set; }
        public int classteacherid { get; set; }
        public string classtypeaccess { get; set; }
        public string roomdescription { get; set; }
        public DateTime termstartdate { get; set; }
        public DateTime termenddate { get; set; }
        public string lessonaccess { get; set; }
        public string groupname { get; set; }
        public string groupaccess { get; set; }
        public string groupinvitetag { get; set; }
        public string groupdescription { get; set; }
        public string teachername { get; set; }
        public string teachersurname { get; set; }
        public string teacherpatronymic { get; set; }
        public string teacherphone { get; set; }
        public string teacherrmail { get; set; }
    }   
}