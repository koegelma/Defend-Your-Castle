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
    public GameObject standardTurretPrefab;
    public GameObject firstTurretPrefab;
    public GameObject missileLauncherPrefab;
    private UnitBlueprint unitToBuild;

    public bool CanBuild { get { return unitToBuild != null; } }

    public void BuildUnitOn(Node node)
    {
        if (PlayerStats.Money < unitToBuild.cost)
        {
            Debug.Log("Not enough money to build that unit!");
            return;
        }

        PlayerStats.Money -= unitToBuild.cost;

        Debug.Log("Unit build! Money left: " + PlayerStats.Money);

        GameObject unit = (GameObject)Instantiate(unitToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.unit = unit;
    }
    public void SelectUnitToBuild(UnitBlueprint turret)
    {
        unitToBuild = turret;
    }
}
