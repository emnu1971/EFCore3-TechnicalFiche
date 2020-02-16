namespace FAVV.TF.EFCore3Mappings.Domain
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Course domain entity class.
    /// RelationType: OneToOneParent (Teacher)
    ///               OneToMany (Student) represented by ManyToMany (Enrollment)
    /// Rules       : - A Course has Zero or One Teacher.
    ///               - A Course can exist without a Teacher.
    /// Info        : TeachedBy has a OneToOne relationship with course
    ///               Teacher will be the child relationship for Course
    ///               Teacher class should have the CourseId set as FK
    ///               Teacher can not exist without course and should always
    ///               be created in context of a course.
    /// </summary>
    public class Course : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual int Credits { get; protected set; }
        public virtual Teacher TeachedBy { get; protected set; } = null;

        protected Course() { }

        public Course(string name, int credits) : this()
        {
            Name = name;
            Credits = credits;
        }

        public Course(string name, int credits, Teacher teacher) : this(name, credits)
        {
            TeachedBy = teacher;
        }

        public virtual void AssignTeacherToCourse(Teacher teacher) =>
            TeachedBy = teacher;


        public virtual void RemoveTeacherFromCourse() =>
            TeachedBy = null;

        public override string ToString()
        {
            return $"{nameof(Course)} --> CourseName: {Name} Credits: {Credits}";
        }

    }
}
