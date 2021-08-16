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
    private Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild { get { return unitToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= unitToBuild.cost; } }

    public void SelectNode(Node node)
    {

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        unitToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectUnitToBuild(UnitBlueprint unit)
    {
        unitToBuild = unit;
        DeselectNode();
    }

    public UnitBlueprint GetUnitToBuild()
    {
        return unitToBuild;
    }
}
