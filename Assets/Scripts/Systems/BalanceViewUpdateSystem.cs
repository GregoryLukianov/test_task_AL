using Leopotam.Ecs;

public class BalanceViewUpdateSystem : IEcsRunSystem
{
    private BalanceSystem _balanceSystem;
    private BalanceView _balanceView;

    public BalanceViewUpdateSystem(BalanceView balanceView)
    {
        _balanceView = balanceView;
    }

    public void Run()
    {
        _balanceView.SetBalance(_balanceSystem.GetBalance());
    }
}