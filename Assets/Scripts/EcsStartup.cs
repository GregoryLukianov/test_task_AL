using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;

    [SerializeField] private BusinessView[] _businessViews;
    [SerializeField] private BalanceView _balanceView;
    [SerializeField] private BusinessConfigProvider _businessConfigProvider;
    [SerializeField] private BusinessViewsProvider _businessViewsProvider;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        var balanceSystem = new BalanceSystem();
        _systems
            .Add(new BusinessIncomeSystem())
            .Add(new BusinessUpgradeSystem())
            .Add(new BusinessLevelUpSystem())
            .Add(new BusinessViewUpdateSystem())
            .Add(new BalanceViewUpdateSystem(_balanceView))
            .Add(new BusinessServiceStartupSystem())
            .Add(balanceSystem)
            .Inject(_businessConfigProvider)
            .Inject(_businessViewsProvider)
            .Inject(new BusinessService())
            .Inject(balanceSystem);
            
        

        
        CreateInitialEntities();

        _systems.Init();
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }

    private void CreateInitialEntities()
    {
        
        var balanceEntity = _world.NewEntity();
        balanceEntity.Get<BalanceComponent>().Value = 0;
        
        foreach (var businessData in _businessConfigProvider.Config.Businesses)
        {
            var businessEntity = _world.NewEntity();
            ref var businessComponent = ref businessEntity.Get<BusinessComponent>();
            businessComponent.Id = businessData.Id;
            businessComponent.Level = businessData.StartingLevel;
            businessComponent.Upgrade1Purchased = false;
            businessComponent.Upgrade2Purchased = false;

            businessEntity.Get<BusinessIncomeTimerComponent>();
        }
    }
}
