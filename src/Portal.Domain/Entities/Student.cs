namespace Portal.Domain.Entities
{
    public class Student : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
