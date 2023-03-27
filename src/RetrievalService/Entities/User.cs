using RetrievalService.Entities.Basic;

namespace RetrievalService.Entities;

public class User : IEntity
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
