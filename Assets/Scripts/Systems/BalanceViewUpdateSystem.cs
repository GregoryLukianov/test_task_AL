using Leopotam.Ecs;

public class BalanceViewUpdateSystem : IEcsInitSystem
{
    private BalanceSystem _balanceSystem;
    private BalanceView _balanceView;

    public BalanceViewUpdateSystem(BalanceView balanceView)
    {
        _balanceView = balanceView;
    }
    
    public void Init()
    {
        _balanceSystem.OnBalanceChangeEvent+=UpdateBalanceText;
    }

    public void UpdateBalanceText()
    {
        _balanceView.SetBalance(_balanceSystem.GetBalance());
    }
}