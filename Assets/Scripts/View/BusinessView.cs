using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BusinessView : MonoBehaviour
{
    public int BusinessId;

    public TextMeshProUGUI NameText;
    public Image Timer;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI IncomeText;
    public UpgradeButtonView Upgrade1Button;
    public UpgradeButtonView Upgrade2Button;
    public UpgradeButtonView LevelUpButton;
    private BusinessService _businessService;

    private Dictionary<UpgradeType, UpgradeButtonView> _upgradeButtons;
    
    public void Awake( )
    {
        _upgradeButtons = new Dictionary<UpgradeType, UpgradeButtonView>();
        _upgradeButtons.Add(UpgradeType.Upgrade1, Upgrade1Button);
        _upgradeButtons.Add(UpgradeType.Upgrade2, Upgrade2Button);
    }

    
    public void UpdateView(BusinessComponent business, BusinessIncomeTimerComponent timer, BusinessConfig config, BusinessInfo businessInfo)
    {
        NameText.text = businessInfo.Name;
        Timer.fillAmount = timer.CurrentTime / config.IncomeDelay;
        LevelText.text = $"LVL:\n{business.Level}";

        float upgradeMultiplier = 0f;
        foreach (var keyValue in business.PurchasedUpgradesDictionary)
        {
            if(keyValue.Value)
                upgradeMultiplier += config.Upgrades.Find(
                    upgrade=>upgrade.UpgradeType== keyValue.Key).IncomeMultiplier;
        }

        float income = business.Level * config.BaseIncome * (1f + upgradeMultiplier/100);
        IncomeText.text = $"Доход:\n{income:F0}$";
        
        if(business.Level == 0)
            IncomeText.text = $"Доход:\n{config.BaseIncome:F0}$";

        
        int nextLevel = business.Level + 1;
        float nextLevelCost = nextLevel * config.BasePrice;
        LevelUpButton.Text.text = $"LEVEL UP$\n" +
                                  $"Цена: {nextLevelCost:F0}$";
        
        
        foreach (var keyButton in _upgradeButtons)
        {
            if (business.PurchasedUpgradesDictionary[keyButton.Key])
            {
                Upgrade1Button.Button.interactable = false;
                Upgrade1Button.Text.text = $"{businessInfo.Upgrades.Find(upgrade => upgrade.UpgradeType ==keyButton.Key).Name}\n" +
                                           $"Доход: + {config.Upgrades.Find(upgrade => upgrade.UpgradeType ==keyButton.Key).IncomeMultiplier}%\n" +
                                           $"Куплено";
            }
            else
            {
                keyButton.Value.Button.interactable = true;
                keyButton.Value.Text.text = 
                    $"{businessInfo.Upgrades.Find(upgrade => upgrade.UpgradeType ==keyButton.Key).Name}\n" +
                    $"Доход: + {config.Upgrades.Find(upgrade => upgrade.UpgradeType ==keyButton.Key).IncomeMultiplier}%\n" +
                    $"Цена: {config.Upgrades.Find(upgrade => upgrade.UpgradeType ==keyButton.Key).Price:F0}$";
            }
        }
        
    }

    public void UpdateLevelText(BusinessComponent business, BusinessConfig config)
    {
        LevelText.text = $"LVL:\n{business.Level}";
        int nextLevel = business.Level + 1;
        float nextLevelCost = nextLevel * config.BasePrice;
        LevelUpButton.Text.text = $"LEVEL UP$\n" +
                                  $"Цена: {nextLevelCost:F0}$";
    }

    public void UpdateUpgradeText(UpgradeType upgradeType,BusinessConfig config, BusinessInfo businessInfo)
    {
        _upgradeButtons[upgradeType].Button.interactable = false;
        _upgradeButtons[upgradeType].Text.text = 
            $"{businessInfo.Upgrades.Find(upgrade => upgrade.UpgradeType ==upgradeType).Name}\n" +
            $"Доход: + {config.Upgrades.Find(upgrade => upgrade.UpgradeType ==upgradeType).IncomeMultiplier}%\n" +
            "Куплено";
        
    }

    public void UpdateIncomeText(BusinessComponent business, BusinessConfig config)
    {
        float upgradeMultiplier = 0f;
        foreach (var keyValue in business.PurchasedUpgradesDictionary)
        {
            if(keyValue.Value)
                upgradeMultiplier += config.Upgrades.Find(
                    upgrade=>upgrade.UpgradeType== keyValue.Key).IncomeMultiplier;
        }

        float income = business.Level * config.BaseIncome * (1f + upgradeMultiplier/100);
        IncomeText.text = $"Доход:\n{income:F0}$";
    }

    public void UpdateTimer(BusinessIncomeTimerComponent timer, BusinessConfig config)
    {
        Timer.fillAmount = timer.CurrentTime / config.IncomeDelay;
    }
}