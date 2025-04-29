using Leopotam.Ecs;

public class BusinessServiceStartupSystem : IEcsInitSystem
{
    private BusinessService _businessService;
    private EcsFilter<BusinessComponent> _businessFilter;

    public void Init()
    {
        _businessService.SetFilter(_businessFilter);
    }
}