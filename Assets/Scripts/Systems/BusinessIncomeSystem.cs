using Leopotam.Ecs;
using UnityEngine;

public class BusinessIncomeSystem : IEcsRunSystem
{
    private EcsFilter<BusinessComponent, BusinessIncomeTimerComponent> _businessFilter;
    private BusinessConfigProvider _configProvider;
    private BalanceSystem _balanceSystem;
    private BusinessViewsProvider _viewsProvider;
    
    public void Run()
    {
        foreach (var i in _businessFilter)
        {
            ref var business = ref _businessFilter.Get1(i);
            
            if(business.Level==0)
                return;
            
            ref var timer = ref _businessFilter.Get2(i);

            var businessData = _configProvider.GetBusinessConfigById(business.Id);
            if (businessData == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                continue;
            }

            timer.CurrentTime += Time.deltaTime;
            _viewsProvider.GetViewById(businessData.Id).UpdateTimer(timer,businessData);
            
            
            if (timer.CurrentTime < businessData.IncomeDelay)
                continue;

            timer.CurrentTime = 0f;
            _viewsProvider.GetViewById(businessData.Id).UpdateTimer(timer,businessData);
            
            
            float upgradeMultiplier = 0f;
            foreach (var keyValue in business.PurchasedUpgradesDictionary)
            {
                if(keyValue.Value)
                    upgradeMultiplier += businessData.Upgrades.Find(
                        upgrade=>upgrade.UpgradeType== keyValue.Key).IncomeMultiplier;
            }

            float income = business.Level * businessData.BaseIncome * (1f + upgradeMultiplier/100);

            _balanceSystem.AddBalance(income);
        }
    }
}