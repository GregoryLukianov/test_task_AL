using System;
using Leopotam.Ecs;

public class BalanceSystem : IEcsInitSystem
{
    private EcsFilter<BalanceComponent> _balanceFilter = null;
    private BalanceComponent _balanceComponent;
    public event Action OnBalanceChangeEvent;

    public void Init()
    {
        _balanceComponent = _balanceFilter.Get1(0);
        OnBalanceChangeEvent?.Invoke();
    }

    public float GetBalance()
    {
        return _balanceComponent.Value;
    }

    public void AddBalance(float amount)
    {
        if(amount<0)
            return;
        
        ref var balance = ref _balanceComponent;
        balance.Value += amount;
        OnBalanceChangeEvent?.Invoke();
    }

    public bool TrySpendBalance(float amount)
    {
        ref var balance = ref _balanceComponent;
        if (balance.Value >= amount)
        {
            balance.Value -= amount;
            OnBalanceChangeEvent?.Invoke();
            return true;
        }
        return false;
    }
}