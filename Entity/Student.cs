namespace net6_web_api.Entity
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
