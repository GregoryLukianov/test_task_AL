using System;
using Leopotam.Ecs;
using UnityEngine;

public class BusinessLevelUpSystem : IEcsRunSystem
{
    private EcsFilter<BusinessComponent, BusinessLevelUpRequestComponent> _levelUpRequestFilter;
    private BusinessConfigProvider _configProvider;
    private BalanceSystem _balanceSystem;
    public event Action<int, BusinessConfig> OnBusinessLevelUpEvent;

    public void Run()
    {
        foreach (var i in _levelUpRequestFilter)
        {
            ref var business = ref _levelUpRequestFilter.Get1(i);

            var businessData = _configProvider.GetBusinessConfigById(business.Id);
            if (businessData == null)
            {
                Debug.Log($"Business config not found for id: {business.Id}");
                continue;
            }

            var nextLevelCost = (business.Level + 1) * businessData.BasePrice;

            if (_balanceSystem.TrySpendBalance(nextLevelCost))
            {
                business.Level++;
                OnBusinessLevelUpEvent?.Invoke(business.Id,businessData);
            }
            else
            {
                Debug.Log("Not enough balance to level up the business!");
            }

            _levelUpRequestFilter.GetEntity(i).Del<BusinessLevelUpRequestComponent>();
        }
    }
}