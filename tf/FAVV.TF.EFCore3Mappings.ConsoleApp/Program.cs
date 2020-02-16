using FAVV.TF.EFCore3Mappings.Data;
using FAVV.TF.EFCore3Mappings.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAVV.TF.EFCore3Mappings.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try

            {
                //--- Mapping and Interacting with a Single Table (Student)
                await InitializeStudentsAsync();

                //--- Mapping and Interacting with OneToOne RelationShips (Course & Teacher)
                //await CreateTeacherAndAssignToNewCourseAsync();
                //await InitializeCoursesAsync();
                //await CreateTeacherAndAssignToExistingCourseAsync();
                //await UpdateTeacherFirstNameFromWithinAssignedCourseAsync();
                //await RemoveAssignedTeacherFromCourseAsync();

                //--- Mapping and Interacting with ManyToMany RelationShips (Course & Student)
                //await EnrollExistingStudentsIntoExistingCoursesAsync();
                //await EnrollNewTrackedStudentIntoUntrackedCourseAsync(1);
                //await DisenrollStudentFromCourseAsync();

                //--- Mapping ValueObjects (OwnedTypes)
                //Money m1 = Money.FiveEuroCent;
                //Money m2 = Money.TenEuroCent;
                //Money m3 = Money.TwentyEuroCent;
                //Money m4 = Money.FiftyEuroCent;
                //Money m5 = Money.OneEuroCoin;
                //Money m6 = Money.TwoEuroCoin;
                //Money total = m1 + m2 + m3 + m4 + m5 + m6;
                //Console.WriteLine(total);

            }
            catch (Exception ex)
            {
                StringBuilder sbMessage = new StringBuilder();
                sbMessage.AppendLine(ex.Message);

                if (ex.InnerException != null)
                {
                    sbMessage.AppendLine(ex.InnerException.Message);
                }
                Console.WriteLine(sbMessage.ToString());
            }
            finally
            {
                Console.WriteLine("Hit any key to stop ...");
                Console.ReadLine();
            }
        }

        #region Student
        private static async Task InitializeStudentsAsync()
        {
            using (var context = new CourseManagementContext())
            {
                IList<Student> students = new List<Student>()
                {
                    new Student("Bart","Depezewever"),
                    new Student("Louis","Tabak"),
                    new Student("Gert","Verhond"),
                    new Student("Maggie","Den Bock"),
                    new Student("Goedele","Kiekens")
                };
                await CreateStudentsAsync(students);
                await DisplayStudentsAsync();

            }
        }

        private static async Task CreateStudentsAsync(IEnumerable<Student> students)
        {
            using (var context = new CourseManagementContext())
            {
                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();
            }
        }

        private static async Task DisplayStudentsAsync()
        {
            Console.WriteLine("\r\nOverview of Student:");
            using (var context = new CourseManagementContext())
            {
                var students = await context.Students.ToListAsync();
                foreach (var student in students)
                {
                    Console.WriteLine($"{student}");
                }
            }
        }

        #endregion Student

        #region Course

        /// <summary>
        /// This code will create a simple entity (Course)
        /// and display it.
        /// </summary>
        /// <returns></returns>
        private static async Task InitializeCoursesAsync()
        {
            var courses = new List<Course>()
            {
                new Course("Chemistry",9),
                new Course("Biology",2),
                new Course("Psychology",6),
                new Course("Mathematics",8),
                new Course("Literature",3),
                new Course("Languages",4),
                new Course("Software Development",6),
                new Course("Law",9),
                new Course("Marketing",3),
                new Course("Architecture",8),
                new Course("Business Analyzing",10)
            };
            await CreateCoursesAsync(courses);
            await DisplayCoursesAsync();
        }

        /// <summary>
        /// This code will Create courses.
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
        private static async Task CreateCoursesAsync(IEnumerable<Course> courses)
        {
            using (var context = new CourseManagementContext())
            {
                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }
        }

        private static async Task DisplayCoursesAsync()
        {
            Console.WriteLine("\r\nOverview of Courses:");
            using (var context = new CourseManagementContext())
            {
                var courses = await context.Courses.ToListAsync();
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course}");
                }
            }
        }

        #endregion Course


        #region Course & Teacher

        /// <summary>
        /// This code will create a new Course (parent)
        /// with a new Teacher (child)
        /// and display them
        /// </summary>
        /// <returns></returns>
        private static async Task CreateTeacherAndAssignToNewCourseAsync()
        {
            using (var context = new CourseManagementContext())
            {
                var teacher = new Teacher("Gwendoline", "Rutten");
                var course = new Course("Politics", 1, teacher);
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();
                DisplayCourseWithTeacher(course);
            }
        }

        /// <summary>
        /// This code will update an existing Course (parent)
        /// with a new Teacher (child)
        /// and display them.
        /// </summa
        /// <returns></returns>
        private static async Task CreateTeacherAndAssignToExistingCourseAsync()
        {
            using (var context = new CourseManagementContext())
            {
                var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == 3);
                if (course != null)
                {
                    var teacher = new Teacher("Armand", "Pien");
                    course.AssignTeacherToCourse(teacher);
                    // Attach, so context is notified of modification
                    context.Courses.Attach(course);
                    await context.SaveChangesAsync();
                    DisplayCourseWithTeacher(course);
                }
            }
        }

        /// <summary>
        /// This code will update the firstname of a child object (Teacher)
        /// within a parent opbject (Course) in a OneToOne relationship
        /// </summary>
        /// <returns></returns>
        private static async Task UpdateTeacherFirstNameFromWithinAssignedCourseAsync()
        {
            using (var context = new CourseManagementContext())
            {
                //find the first course which has a teacher attached
                var course = await context.Courses.Include("TeachedBy")
                    .FirstOrDefaultAsync(c => c.TeachedBy != null);

                //if found, change firstname of attached teacher
                if (course != null && course.TeachedBy != null)
                {
                    course.TeachedBy.UpdateFirstName($"{course.TeachedBy.FirstName}_changed");
                }
                await context.SaveChangesAsync();
                DisplayCourseWithTeacher(course);
            }
        }

        /// <summary>
        /// This code will disenroll a teacher from a course
        /// </summary>
        /// <returns></returns>
        private static async Task RemoveAssignedTeacherFromCourseAsync()
        {
            //find a course with a teacher
            using (var context = new CourseManagementContext())
            {
                var course = await context.Courses.Include("TeachedBy").FirstOrDefaultAsync(c => c.TeachedBy != null);

                if (course != null)
                {
                    course.RemoveTeacherFromCourse();
                    context.Attach(course);
                    await context.SaveChangesAsync();
                    DisplayCourseWithTeacher(course);
                }
            }
        }

        private static void DisplayCourseWithTeacher(Course course)
        {
            Console.WriteLine(course);
            Console.WriteLine(course.TeachedBy);
        }

        #endregion Course & Teacher

        #region Student to Course Enrollment/Disenrollment

        private static async Task EnrollExistingStudentsIntoExistingCoursesAsync()
        {
            using (var context = new CourseManagementContext())
            {
                // get a student
                var student = await context.Students.FirstOrDefaultAsync(s => s.Id == 1);
                // get 2 courses to enroll the student into
                var courses = await context.Courses.Where(c => c.Id > 0 && c.Id < 3).ToListAsync();

                foreach (var course in courses)
                {
                    student.Enroll(course, Grade.A);
                }
                context.Attach(student);
                await context.SaveChangesAsync();

                // get the student with his enrolled courses
                var studentWithEnrolledCourses =
                    await context.Students
                    .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                    .FirstOrDefaultAsync(s => s.Id == 1);

                if (studentWithEnrolledCourses != null)
                {
                    DisplayStudentWithEnrollments(studentWithEnrolledCourses);
                }

            }
        }

        /// <summary>
        /// In this case we first load an existing Course which is next
        /// untracked.
        /// Next we creat a new Student (which is tracked) and enroll the
        /// Student into the untracked course.
        /// </summary>
        /// <returns></returns>
        private static async Task EnrollNewTrackedStudentIntoUntrackedCourseAsync(long courseId)
        {
            //get a course first
            Course untrackedCourse;
            using (var context = new CourseManagementContext())
            {
                untrackedCourse = context.Courses.Find(courseId);
            }

            if (untrackedCourse != null)
            {
                using (var context = new CourseManagementContext())
                {
                    var newStudent = new Student("Pana", "Marenko");
                    newStudent.Enroll(untrackedCourse, Grade.B);
                    // we have to attach the untracked course first !
                    context.Attach(untrackedCourse);
                    await context.AddAsync(newStudent);
                    // we have to call detectchanges on the context !
                    context.ChangeTracker.DetectChanges();
                    await context.SaveChangesAsync();

                    DisplayStudentWithEnrollments(newStudent);
                }
            }
            else Console.WriteLine($"Course with Id {courseId} not found!");
        }

        private static async Task DisenrollStudentFromCourseAsync()
        {
            using (var context = new CourseManagementContext())
            {

                // get a student
                var student = await context.Students
                    .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                    .FirstOrDefaultAsync(s => s.Id == 1);

                // show enrolled course before disenrollment
                DisplayStudentWithEnrollments(student);

                if (student != null && student.Enrollments.Count > 0)
                {
                    var courseToDisenrollFrom = student.Enrollments.First();
                    student.RemoveEnrollment(courseToDisenrollFrom, "I Don't like the course !");
                    await context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("No Enrollments found !");
                }

                // show enrolled course after disenrollment
                DisplayStudentWithEnrollments(student);
            }
        }

        private static void DisplayStudentWithEnrollments(Student student)
        {
            Console.WriteLine(student);
            if (student.Enrollments.Count == 0)
            {
                Console.WriteLine($"No Course Enrollments found for Student: {student}");
            }
            else
            {
                foreach (var enrolledCourse in student.Enrollments)
                {
                    Console.WriteLine(enrolledCourse.Course);
                }
            }

        }


        #endregion Student to Course Enrollment
    }
}
