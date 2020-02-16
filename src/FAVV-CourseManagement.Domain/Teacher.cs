namespace FAVV_CourseManagement.Domain
{
    /// <summary>
    /// Author      : Emmanuel Nuyttens
    /// Date        : 01-2020
    /// Purpose     : Teach domain entity class.
    /// RelationType: OneToOneChild (Course)
    /// Rules       : - A Teacher can only teach in a Single course.
    ///               - A Teacher can not exist without a Course.
    ///               - A Teacher can only be created in context of a Course.
    /// Info        : Because CourseId (FK) is set, we do not need a migration
    ///               to explictly set the OneToOne relationship, if you create
    ///               migration, this will be empty.
    ///               Including the (optional) CourseId will create the link with
    ///               the parent table (Course) and set Teacher as child of Course
    /// </summary>
    public class Teacher : Entity
    {

        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }

        /// <summary>
        /// Adding a reference to the Course Id will set 
        /// Teacher as a child of Course and create a
        /// OneToOne relationship between them
        /// We set this relation as optonal so we could
        /// (later) create Teacher as a standalone object
        /// if needed
        /// </summary>
        public virtual int? CourseId { get; protected set; }

        protected Teacher() { }

        public Teacher(string firstName, string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public virtual void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public virtual void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{nameof(Teacher)} --> FirstName: {FirstName} LastName: {LastName}";
        }

    }
}
