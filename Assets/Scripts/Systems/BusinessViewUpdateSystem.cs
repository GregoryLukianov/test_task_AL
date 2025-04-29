using Leopotam.Ecs;
using UnityEngine;

public class BusinessViewUpdateSystem : IEcsInitSystem
{
    private EcsFilter<BusinessComponent, BusinessIncomeTimerComponent> _businessFilter = null;

    private BusinessViewsProvider _viewsProvider;
    private BusinessConfigProvider _configProvider;
    private BusinessService _businessService;
    private BusinessUpgradeSystem _businessUpgradeSystem;
    private BusinessLevelUpSystem _businessLevelUpSystem;

    public void Init()
    {
        _businessUpgradeSystem._onBusinessUpgradedEvent += UpdateUpgradeText;
        _businessLevelUpSystem.OnBusinessLevelUpEvent += UpdateLevelText;
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

            var config = _configProvider.GetBusinessConfigById(businessId);
            if (config == null)
            {
                Debug.LogError($"Business config not found for ID: {businessId}");
                continue;
            }
                
            view.UpdateView(business, timer, config, _configProvider.GetBusinessInfoById(businessId));
                
                
            view.Upgrade1Button.Button.onClick.RemoveAllListeners();
            view.Upgrade2Button.Button.onClick.RemoveAllListeners();
            view.LevelUpButton.Button.onClick.RemoveAllListeners();
                
            view.Upgrade1Button.Button.onClick.AddListener(() => _businessService.RequestUpgrade1(businessId));
            view.Upgrade2Button.Button.onClick.AddListener(() => _businessService.RequestUpgrade2(businessId));
            view.LevelUpButton.Button.onClick.AddListener(() => _businessService.RequestLevelUp(businessId));
        }
    }

    public void UpdateLevelText(int businessId, BusinessConfig businessConfig)
    {
        var view = _viewsProvider.GetViewById(businessId);
        
        view.UpdateLevelText(GetBusinessComponentById(businessId), businessConfig);
        view.UpdateIncomeText(
             GetBusinessComponentById(businessId),
            _configProvider.GetBusinessConfigById(businessId));
    }

    public void UpdateUpgradeText(int businessId, UpgradeType upgradeType)
    {
        var view = _viewsProvider.GetViewById(businessId);
        view.UpdateUpgradeText(
            upgradeType,
            _configProvider.GetBusinessConfigById(businessId),
            _configProvider.GetBusinessInfoById(businessId));
        view.UpdateIncomeText(
            GetBusinessComponentById(businessId),
            _configProvider.GetBusinessConfigById(businessId));
    }

    private BusinessComponent GetBusinessComponentById(int businessId)
    {
        foreach (var i in _businessFilter)
        {
            if (_businessFilter.Get1(i).Id == businessId)
                return _businessFilter.Get1(i);
        }

        return new BusinessComponent();
    }
    
}
