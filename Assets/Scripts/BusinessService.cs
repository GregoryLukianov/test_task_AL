using Leopotam.Ecs;
using UnityEngine;

public class BusinessService
{
    private EcsFilter<BusinessComponent> _businessFilter;
    
    
    public void SetFilter(EcsFilter<BusinessComponent> filter)
    {
        _businessFilter = filter;
    }

    public BusinessComponent? GetBusinessById(int id)
    {
        foreach (var i in _businessFilter)
        {
            if (_businessFilter.Get1(i).Id == id)
                return _businessFilter.Get1(i);
        }
        return null;
    }
    
    private void AddRequest<T>(int businessId) where T : struct
    {
        foreach (var i in _businessFilter)
        {
            ref var business = ref _businessFilter.Get1(i);

            if (business.Id == businessId)
            {
                var entity = _businessFilter.GetEntity(i);

                if (!entity.Has<T>())
                {
                    entity.Get<T>();
                }

                return;
            }
        }

        Debug.Log($"Business with ID {businessId} not found for request of type {typeof(T).Name}!");
    }
    
    public void RequestUpgrade1(int businessId) => AddRequest<BusinessUpgrade1RequestComponent>(businessId);
    public void RequestUpgrade2(int businessId) => AddRequest<BusinessUpgrade2RequestComponent>(businessId);
    public void RequestLevelUp(int businessId) => AddRequest<BusinessLevelUpRequestComponent>(businessId);
}