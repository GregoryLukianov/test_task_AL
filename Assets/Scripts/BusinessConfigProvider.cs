using System.Collections.Generic;
using UnityEngine;

public class BusinessConfigProvider : MonoBehaviour
{
    [SerializeField] private BusinessConfig _config;

    private Dictionary<int, BusinessConfig.BusinessData> _businessesById;

    public BusinessConfig Config => _config;

    private void Awake()
    {
        _businessesById = new Dictionary<int, BusinessConfig.BusinessData>();

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
    }

    public BusinessConfig.BusinessData GetBusinessDataById(int id)
    {
        if (_businessesById.TryGetValue(id, out var data))
        {
            return data;
        }
        
        Debug.Log($"BusinessData with id {id} not found!");
        return null;
    }
}