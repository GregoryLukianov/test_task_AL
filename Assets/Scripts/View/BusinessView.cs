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
    
    public void Init(BusinessService businessService)
    {
        _businessService = businessService;

        Upgrade1Button.Button.onClick.AddListener(() => _businessService.RequestUpgrade1(BusinessId));
        Upgrade2Button.Button.onClick.AddListener(() => _businessService.RequestUpgrade2(BusinessId));
        LevelUpButton.Button.onClick.AddListener(() => _businessService.RequestLevelUp(BusinessId));
    }

    
    public void UpdateView(BusinessComponent business, BusinessIncomeTimerComponent timer, BusinessConfig.BusinessData config)
    {
        NameText.text = config.Name;
        Timer.fillAmount = timer.CurrentTime / config.IncomeDelay;
        LevelText.text = $"LVL:\n{business.Level}";

        float upgradeMultiplier = 0f;
        if (business.Upgrade1Purchased)
            upgradeMultiplier += config.Upgrade1.IncomeMultiplier;
        if (business.Upgrade2Purchased)
            upgradeMultiplier += config.Upgrade2.IncomeMultiplier;

        float income = business.Level * config.BaseIncome * (1f + upgradeMultiplier/100);
        IncomeText.text = $"Доход:\n{income:F0}$";
        
        if(business.Level == 0)
            IncomeText.text = $"Доход:\n{config.BaseIncome:F0}$";

        
        int nextLevel = business.Level + 1;
        float nextLevelCost = nextLevel * config.BasePrice;
        LevelUpButton.Text.text = $"LEVEL UP$\n" +
                                  $"Цена: {nextLevelCost:F0}$";
        
        if (business.Upgrade1Purchased)
        {
            Upgrade1Button.Button.interactable = false;
            Upgrade1Button.Text.text = $"{config.Upgrade1.Name}\n" +
                                       $"Доход: + {config.Upgrade1.IncomeMultiplier}%\n" +
                                       $"Куплено";
        }
        else
        {
            Upgrade1Button.Button.interactable = true;
            Upgrade1Button.Text.text = $"{config.Upgrade1.Name}\n" +
                                       $"Доход: + {config.Upgrade1.IncomeMultiplier}%\n" +
                                       $"Цена: {config.Upgrade1.Price:F0}$";
        }
        
        if (business.Upgrade2Purchased)
        {
            Upgrade2Button.Button.interactable = false;
            Upgrade2Button.Text.text = $"{config.Upgrade2.Name}\n" +
                                       $"Доход: + {config.Upgrade2.IncomeMultiplier}%\n" +
                                       $"Куплено";
        }
        else
        {
            Upgrade2Button.Button.interactable = true;
            Upgrade2Button.Text.text = $"{config.Upgrade2.Name}\n" +
                                       $"Доход: + {config.Upgrade2.IncomeMultiplier}%\n" +
                                       $"Цена: {config.Upgrade2.Price:F0}$";
        }
    }
}