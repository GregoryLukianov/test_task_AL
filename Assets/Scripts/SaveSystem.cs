using System;
using System.Collections.Generic;
using Leopotam.Ecs;

public class SaveLoadSystem: IEcsInitSystem
{
    private EcsFilter<BalanceComponent> _balanceFilter = null;
    private EcsFilter<BusinessComponent, BusinessIncomeTimerComponent> _businessFilter = null;
    private BusinessViewsProvider _viewsProvider;
    private BusinessConfigProvider _configProvider;
    
    public void Init()
    {
        var gameData = DataManager.LoadData();
        if (gameData.BusinessDatas.Count == 0)
            return;

        _balanceFilter.Get1(0).Value = gameData.Balance.Value;
        foreach (var i in _businessFilter)
        {
            ref var business = ref _businessFilter.Get1(i);
            ref var timer = ref _businessFilter.Get2(i);
            var businessId = business.Id;
            var businessData = gameData.BusinessDatas.Find(BusinessData => BusinessData.Id == businessId);
            business.Level = businessData.Business.Level;
            business.PurchasedUpgradesDictionary = businessData.Business.PurchasedUpgradesDictionary;

            timer.CurrentTime = businessData.IncomeTimer.CurrentTime;
            
            _viewsProvider.GetViewById(businessId).UpdateView(
                business,
                timer,
                _configProvider.GetBusinessConfigById(businessId),
                _configProvider.GetBusinessInfoById(businessId));
        }
    }

    public void Save()
    {
        var gameData = new GameData();
        gameData.Balance = _balanceFilter.Get1(0);
        foreach (var i in _businessFilter)
        {
            var data = new BusinessData();
            ref var business = ref _businessFilter.Get1(i);
            ref var timer = ref _businessFilter.Get2(i);
            var businessId = business.Id;

            data.Id = businessId;
            data.Business = business;
            data.IncomeTimer = timer;
            gameData.BusinessDatas.Add(data);
        }
        DataManager.SaveData(gameData);
    }
}