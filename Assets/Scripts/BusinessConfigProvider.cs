using System.Collections.Generic;
using UnityEngine;

public class BusinessConfigProvider : MonoBehaviour
{
    [SerializeField] private BusinessesConfig _config;
    [SerializeField] private BusinessesInfo _info;

    private Dictionary<int, BusinessConfig> _businessesById;
    private Dictionary<int, BusinessInfo> _businessesInfoById;

    public BusinessesConfig Config => _config;

    private void Awake()
    {
        _businessesById = new Dictionary<int, BusinessConfig>();

        foreach (var businessData in _config.Businesses)
        {
            if (!_businessesById.ContainsKey(businessData.Id))
            {
                _businessesById.Add(businessData.Id, businessData);
            }
            else
            {
                Debug.Log($"Duplicate Business ID detected: {businessData.Id}");
            }
        }
        
        _businessesInfoById = new Dictionary<int, BusinessInfo>();

        foreach (var businessInfo in _info.BusinessesInfos)
        {
            if (!_businessesInfoById.ContainsKey(businessInfo.Id))
            {
                _businessesInfoById.Add(businessInfo.Id, businessInfo);
            }
            else
            {
                Debug.Log($"Duplicate Business ID detected: {businessInfo.Id}");
            }
        }
    }

    public BusinessConfig GetBusinessConfigById(int id)
    {
        if (_businessesById.TryGetValue(id, out var data))
        {
            return data;
        }
        
        Debug.Log($"BusinessData with id {id} not found!");
        return null;
    }
    
    public BusinessInfo GetBusinessInfoById(int id)
    {
        if (_businessesInfoById.TryGetValue(id, out var data))
        {
            return data;
        }
        
        Debug.Log($"BusinessData with id {id} not found!");
        return null;
    }
}