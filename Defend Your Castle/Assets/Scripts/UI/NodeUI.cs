using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Text sellAmount;
    private Node target;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.unitBlueprint.upgradeCost + "G";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellAmount.text = target.unitBlueprint.GetSellAmount() + "G";

        //target.SetRadiusActive();
        ui.SetActive(true);

    }

    public void Hide()
    {
        //target.SetRadiusDeactive();
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeUnit();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellUnit();
        BuildManager.instance.DeselectNode();
    }
}
