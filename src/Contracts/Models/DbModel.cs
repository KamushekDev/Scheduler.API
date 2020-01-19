using System;
using Contracts.Enums;

namespace Contracts.Models
{
    public class DbModel
    {
        public int classid { get; set; }
        public string lessonname { get; set; }
        public string roomname { get; set; }
        public int groupid { get; set; }
        public TimeSpan classtime { get; set; }
        public AccessModifier classaccess { get; set; }
        public string classtypename { get; set; }
        public int classtermid { get; set; }
        public int classteacherid { get; set; }
        public int classduration { get; set; }
        public string classtypedescription { get; set; }
        public string roomdescription { get; set; }
        public DateTime termstartdate { get; set; }
        public DateTime termenddate { get; set; }
        public string termdescription { get; set; }
        public string lessondescription { get; set; }
        public string groupname { get; set; }
        public AccessModifier groupaccess { get; set; }
        public string groupinvitetag { get; set; }
        public string groupdescription { get; set; }
        public string teachername { get; set; }
        public string teachersurname { get; set; }
        public string teacherpatronymic { get; set; }
        public string teacherphone { get; set; }
        public string teacheremail { get; set; }
    }   
}