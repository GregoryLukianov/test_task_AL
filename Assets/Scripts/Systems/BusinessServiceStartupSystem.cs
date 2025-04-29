using Leopotam.Ecs;

public class BusinessServiceStartupSystem : IEcsInitSystem
{
    private BusinessService _businessService = null;
    private EcsFilter<BusinessComponent> _businessFilter = null;

    public void Init()
    {
        _businessService.SetFilter(_businessFilter);
    }
}