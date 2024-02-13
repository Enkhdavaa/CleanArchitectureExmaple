using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain;
using MediatR;

namespace GymManagement.Application;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(IUnitOfWork unitOfWork, IGymRepository gymRepository)
    {
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
    }

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        var gym = new Gym(request.Name, request.SubscriptionId);

        await _gymRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}
