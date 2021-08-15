using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    private UnitBlueprint unitToBuild;

    public bool CanBuild { get { return unitToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= unitToBuild.cost; } }
    public void BuildUnitOn(Node node)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that unit!");
            return;
        }

        PlayerStats.Money -= unitToBuild.cost;

        //Debug.Log("Unit build! Money left: " + PlayerStats.Money);

        GameObject unit = (GameObject)Instantiate(unitToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.unit = unit;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public void SelectUnitToBuild(UnitBlueprint unit)
    {
        unitToBuild = unit;
    }
}
