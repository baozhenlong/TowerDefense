using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI sellWorthText;
    public Button upgradeButton;
    private Node targetComp;

    public void SetTarget(Node _targetComp)
    {
        targetComp = _targetComp;
        transform.position = targetComp.BuildPosition;
        if (targetComp.isUpgraded)
        {
            upgradeCostText.text = "Done";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeCostText.text = "$" + targetComp.turretBlueprint.upgradedCost;
            upgradeButton.interactable = true;
        }
        sellWorthText.text = "$" + targetComp.turretBlueprint.Worth;
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void OnClickUpgrade()
    {
        targetComp.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void OnClickSell()
    {
        targetComp.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
