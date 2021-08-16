using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color insufficientMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject unit;
    [HideInInspector]
    public UnitBlueprint unitBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void BuildUnit(UnitBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that unit!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        //Debug.Log("Unit build! Money left: " + PlayerStats.Money);

        GameObject _unit = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        unit = _unit;

        unitBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeUnit()
    {
        if (PlayerStats.Money < unitBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that unit!");
            return;
        }

        PlayerStats.Money -= unitBlueprint.upgradeCost;

        Destroy(unit);

        //Debug.Log("Unit build! Money left: " + PlayerStats.Money);

        GameObject _unit = (GameObject)Instantiate(unitBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        unit = _unit;


        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellUnit()
    {
        PlayerStats.Money += unitBlueprint.GetSellAmount();
        Destroy(unit);
        unitBlueprint = null;
        isUpgraded = false;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (unit != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) return;

        BuildUnit(buildManager.GetUnitToBuild());
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !buildManager.CanBuild) return;

        if (buildManager.HasMoney) rend.material.color = hoverColor;
        else rend.material.color = insufficientMoneyColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
