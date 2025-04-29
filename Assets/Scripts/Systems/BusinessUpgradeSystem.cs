using Leopotam.Ecs;
using UnityEngine;

public class BusinessUpgradeSystem : IEcsRunSystem
{
    private EcsFilter<BusinessComponent, BusinessUpgrade1RequestComponent> _upgrade1Filter = null;
    private EcsFilter<BusinessComponent, BusinessUpgrade2RequestComponent> _upgrade2Filter = null;
    private BusinessConfigProvider _configProvider;
    private BalanceSystem _balanceSystem;

    public void Run()
    {
        foreach (var i in _upgrade1Filter)
        {
            ref var business = ref _upgrade1Filter.Get1(i);
            var entity = _upgrade1Filter.GetEntity(i);

            if (business.Level == 0 || business.Upgrade1Purchased)
            {
                entity.Del<BusinessUpgrade1RequestComponent>();
                continue;
            }

            var data = _configProvider.GetBusinessDataById(business.Id);
            if (data == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                entity.Del<BusinessUpgrade1RequestComponent>();
                continue;
            }

            if (_balanceSystem.TrySpendBalance(data.Upgrade1.Price))
                business.Upgrade1Purchased = true;
            else
                Debug.Log("Not enough balance to buy Upgrade 1!");

            entity.Del<BusinessUpgrade1RequestComponent>();
        }

        foreach (var i in _upgrade2Filter)
        {
            ref var business = ref _upgrade2Filter.Get1(i);
            var entity = _upgrade2Filter.GetEntity(i);

            if (business.Level == 0 || business.Upgrade2Purchased)
            {
                entity.Del<BusinessUpgrade2RequestComponent>();
                continue;
            }

            var data = _configProvider.GetBusinessDataById(business.Id);
            if (data == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                entity.Del<BusinessUpgrade2RequestComponent>();
                continue;
            }

            if (_balanceSystem.TrySpendBalance(data.Upgrade2.Price))
                business.Upgrade2Purchased = true;
            else
                Debug.Log("Not enough balance to buy Upgrade 2!");

            entity.Del<BusinessUpgrade2RequestComponent>();
        }
    }
}
