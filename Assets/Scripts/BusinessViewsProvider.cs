using System.Collections.Generic;
using UnityEngine;

public class BusinessViewsProvider : MonoBehaviour
{
    public List<BusinessView> Views;

    private Dictionary<int, BusinessView> _viewsById;

    private void Awake()
    {
        _viewsById = new Dictionary<int, BusinessView>();

        foreach (var view in Views)
        {
            _viewsById[view.BusinessId] = view;
        }
    }

    public BusinessView GetViewById(int businessId)
    {
        if (_viewsById.TryGetValue(businessId, out var view))
        {
            return view;
        }

        Debug.Log($"BusinessView not found for id {businessId}");
        return null;
    }
}