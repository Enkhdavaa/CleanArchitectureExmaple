namespace GymManagement.Domain;

public class Gym
{
    private readonly string Name;
    private readonly Guid _id;

    public Gym(string name, Guid id)
    {
        Name = name;
        _id = id;
    }

    private Gym()
    {

    }
}
