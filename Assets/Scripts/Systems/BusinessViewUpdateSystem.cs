using Leopotam.Ecs;
using UnityEngine;

public class BusinessViewUpdateSystem : IEcsRunSystem
{
    private EcsFilter<BusinessComponent, BusinessIncomeTimerComponent> _businessFilter = null;

    private BusinessViewsProvider _viewsProvider;
    private BusinessConfigProvider _configProvider;
    private BusinessService _businessService;

    public void Run()
    {
        foreach (var i in _businessFilter)
        {
            ref var business = ref _businessFilter.Get1(i);
            ref var timer = ref _businessFilter.Get2(i);
            var businessId = business.Id;

            var view = _viewsProvider.GetViewById(businessId);
            if (view == null)
            {
                Debug.LogError($"BusinessView not found for Business ID: {businessId}");
                continue;
            }

            var config = _configProvider.GetBusinessDataById(businessId);
            if (config == null)
            {
                Debug.LogError($"Business config not found for ID: {businessId}");
                continue;
            }
                
            view.UpdateView(business, timer, config);
                
                
            view.Upgrade1Button.Button.onClick.RemoveAllListeners();
            view.Upgrade2Button.Button.onClick.RemoveAllListeners();
            view.LevelUpButton.Button.onClick.RemoveAllListeners();
                
            view.Upgrade1Button.Button.onClick.AddListener(() => _businessService.RequestUpgrade1(businessId));
            view.Upgrade2Button.Button.onClick.AddListener(() => _businessService.RequestUpgrade2(businessId));
            view.LevelUpButton.Button.onClick.AddListener(() => _businessService.RequestLevelUp(businessId));
        }
    }
}
