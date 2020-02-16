using System;

namespace FAVV.TF.EFCore3Mappings.Domain
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Disenrollment domain entity class.
    /// RelationType: ManyToMany
    /// Rules       : - A Disenrollment must have 1 Course, 1 Student, Date of Disenrollment and a Comment.
    ///               - A Disenrollment disenrolls a Student from an enrolled course on a given date.
    ///               - A Student who is disenrolled from an enrolled Course must supply a disenrollment Comment.
    /// </summary>
    public class Disenrollment : Entity
    {
        public virtual long StudentId { get; protected set; }
        public virtual Student Student { get; protected set; }

        public virtual long CourseId { get; protected set; }
        public virtual Course Course { get; protected set; }
        public virtual DateTime DateTime { get; protected set; }
        public virtual string Comment { get; protected set; }

        protected Disenrollment()
        {
        }

        public Disenrollment(Student student, Course course, string comment)
            : this()
        {
            Student = student;StudentId = student.Id;
            Course = course;CourseId = course.Id;
            Comment = comment;
            DateTime = DateTime.UtcNow;
        }
    }
}
