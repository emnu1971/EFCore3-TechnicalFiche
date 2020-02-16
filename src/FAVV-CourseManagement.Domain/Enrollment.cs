namespace FAVV_CourseManagement.Domain
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Enrollment domain entity class. 
    /// RelationType: ManyToMany
    /// Rules       : - An Enrollment must have 1 Course, 1 Student & 1 Grade.
    ///               - An Enrollment Enrolles a Student into a Single Course.
    ///               - A Student enrolled in a Course has a Grade for that Enrollment.
    /// </summary>
    public class Enrollment : Entity
    {
        public virtual long StudentId { get; protected set; }
        public virtual Student Student { get; protected set; }
        public virtual long CourseId { get; protected set; }
        public virtual Course Course { get; protected set; }
        public virtual Grade Grade { get; protected set; }


        protected Enrollment()
        {
        }

        public Enrollment(Student student, Course course, Grade grade)
            : this()
        {
            Student = student;
            StudentId = student.Id;

            Course = course;
            CourseId = course.Id;

            Grade = grade;
        }

        public virtual void Update(Course course, Grade grade)
        {
            Course = course;
            Grade = grade;
        }

        public override string ToString()
        {
            return $"{nameof(Enrollment)} --> {Course}";
        }
    }

    public enum Grade
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        F = 5
    }
}
