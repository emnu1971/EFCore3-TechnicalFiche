using System;
using System.Collections.Generic;
using System.Linq;

namespace FAVV_CourseManagement.Domain
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Student domain entity class.
    /// RelationType: OneToMany (Course) represented by ManyToMany (Enrollment)
    /// Rules       : - A Student can exist without a Course attached to it.
    ///               - A Student can enroll into ZeroOrManyCourses
    /// </summary>
    public class Student : Entity
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }

        public virtual List<Enrollment> Enrollments {get; protected set;}
        public virtual List<Disenrollment> Disenrollments { get; protected set; }

        protected Student() 
        {
            Enrollments = new List<Enrollment>();
            Disenrollments = new List<Disenrollment>();
        }

        public Student(string firstName, string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }


        public virtual Enrollment GetEnrollment(int index)
        {
            if (Enrollments.Count > index)
                return Enrollments[index];

            return null;
        }

        public virtual void Enroll(Course course, Grade grade)
        {
            if (Enrollments.Count >= 2)
                throw new Exception("Cannot have more than 2 enrollments");

            var enrollment = new Enrollment(this, course, grade);
            Enrollments.Add(enrollment);
        }

        public virtual void RemoveEnrollment(Enrollment enrollment, string comment)
        {
            Enrollments.Remove(enrollment);

            var disenrollment = new Disenrollment(enrollment.Student, enrollment.Course, comment);
            Disenrollments.Add(disenrollment);
        }


        public override string ToString()
        {
            return $"{nameof(Student)} --> FirstName: {FirstName}, LastName: {LastName}";
        }

    }

}
