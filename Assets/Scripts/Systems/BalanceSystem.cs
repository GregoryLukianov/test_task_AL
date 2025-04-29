using Leopotam.Ecs;

public class BalanceSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private EcsEntity _balanceEntity;

    public void Init()
    {
        _balanceEntity = _world.NewEntity();
        _balanceEntity.Get<BalanceComponent>().Value = 0f;
    }

    public float GetBalance()
    {
        return _balanceEntity.Get<BalanceComponent>().Value;
    }

    public void AddBalance(float amount)
    {
        if(amount<0)
            return;
        
        ref var balance = ref _balanceEntity.Get<BalanceComponent>();
        balance.Value += amount;
    }

    public bool TrySpendBalance(float amount)
    {
        ref var balance = ref _balanceEntity.Get<BalanceComponent>();
        if (balance.Value >= amount)
        {
            balance.Value -= amount;
            return true;
        }
        return false;
    }
}