using Leopotam.Ecs;
using UnityEngine;

public class BusinessIncomeSystem : IEcsRunSystem
{
    private EcsFilter<BusinessComponent, BusinessIncomeTimerComponent> _businessFilter = null;
    private BusinessConfigProvider _configProvider;
    private BalanceSystem _balanceSystem;
    
    public void Run()
    {
        foreach (var i in _businessFilter)
        {
            ref var business = ref _businessFilter.Get1(i);
            
            if(business.Level==0)
                return;
            
            ref var timer = ref _businessFilter.Get2(i);

            var businessData = _configProvider.GetBusinessDataById(business.Id);
            if (businessData == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                continue;
            }

            timer.CurrentTime += Time.deltaTime;

            if (timer.CurrentTime < businessData.IncomeDelay)
                continue;

            timer.CurrentTime = 0f;

            float upgradeMultiplier = 0f;
            if (business.Upgrade1Purchased)
                upgradeMultiplier += businessData.Upgrade1.IncomeMultiplier;
            if (business.Upgrade2Purchased)
                upgradeMultiplier += businessData.Upgrade2.IncomeMultiplier;

            float income = business.Level * businessData.BaseIncome * (1f + upgradeMultiplier/100);

            _balanceSystem.AddBalance(income);
        }
    }
}