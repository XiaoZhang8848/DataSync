namespace ParseService.Attributes;

public class QueueAttribute : Attribute
{
    public QueueAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}