using GymManagement.Domain;

namespace GymManagement.Application;

public interface IGymRepository
{
    Task AddGymAsync(Gym gym);
    Task GetByIdAsync(Guid id);
    Task RemoveGymAsync(Gym gym);
}
