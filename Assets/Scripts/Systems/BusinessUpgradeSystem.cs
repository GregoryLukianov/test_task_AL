using System;
using Leopotam.Ecs;
using UnityEngine;

public class BusinessUpgradeSystem : IEcsRunSystem
{
    private EcsFilter<BusinessComponent, BusinessUpgrade1RequestComponent> _upgrade1Filter;
    private EcsFilter<BusinessComponent, BusinessUpgrade2RequestComponent> _upgrade2Filter;
    private BusinessConfigProvider _configProvider;
    private BalanceSystem _balanceSystem;
    public event Action<int, UpgradeType> OnBusinessUpgradedEvent;

    public void Run()
    {
        foreach (var i in _upgrade1Filter)
        {
            ref var business = ref _upgrade1Filter.Get1(i);
            var entity = _upgrade1Filter.GetEntity(i);

            if (business.Level == 0 || business.PurchasedUpgradesDictionary[UpgradeType.Upgrade1])
            {
                entity.Del<BusinessUpgrade1RequestComponent>();
                continue;
            }

            var data = _configProvider.GetBusinessConfigById(business.Id);
            if (data == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                entity.Del<BusinessUpgrade1RequestComponent>();
                continue;
            }

            if (_balanceSystem.TrySpendBalance(data.Upgrades[0].Price))
            {
                business.PurchasedUpgradesDictionary[UpgradeType.Upgrade1] = true;
                OnBusinessUpgradedEvent?.Invoke(business.Id, UpgradeType.Upgrade1);
            }
            else
                Debug.Log("Not enough balance to buy Upgrade 1!");

            entity.Del<BusinessUpgrade1RequestComponent>();
        }

        foreach (var i in _upgrade2Filter)
        {
            ref var business = ref _upgrade2Filter.Get1(i);
            var entity = _upgrade2Filter.GetEntity(i);

            if (business.Level == 0 || business.PurchasedUpgradesDictionary[UpgradeType.Upgrade2])
            {
                entity.Del<BusinessUpgrade2RequestComponent>();
                continue;
            }

            var data = _configProvider.GetBusinessConfigById(business.Id);
            if (data == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                entity.Del<BusinessUpgrade2RequestComponent>();
                continue;
            }

            if (_balanceSystem.TrySpendBalance(data.Upgrades[1].Price))
            {
                business.PurchasedUpgradesDictionary[UpgradeType.Upgrade2]= true;
                OnBusinessUpgradedEvent?.Invoke(business.Id,UpgradeType.Upgrade2);
            }
            else
                Debug.Log("Not enough balance to buy Upgrade 2!");

            entity.Del<BusinessUpgrade2RequestComponent>();
        }
    }
}
