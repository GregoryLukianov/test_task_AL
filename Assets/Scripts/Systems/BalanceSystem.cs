using System;
using Leopotam.Ecs;

public class BalanceSystem : IEcsInitSystem
{
    private EcsFilter<BalanceComponent> _balanceFilter;
    public event Action OnBalanceChangeEvent;

    public void Init()
    {
        OnBalanceChangeEvent?.Invoke();
    }

    public float GetBalance()
    {
        return _balanceFilter.Get1(0).Value;
    }

    public void AddBalance(float amount)
    {
        if(amount<0)
            return;
        
        ref var balance = ref _balanceFilter.Get1(0);
        balance.Value += amount;
        OnBalanceChangeEvent?.Invoke();
    }

    public bool TrySpendBalance(float amount)
    {
        ref var balance = ref _balanceFilter.Get1(0);
        if (balance.Value >= amount)
        {
            balance.Value -= amount;
            OnBalanceChangeEvent?.Invoke();
            return true;
        }
        return false;
    }
}