using UnityEngine;

public class Shop : MonoBehaviour
{
    public UnitBlueprint standardTurret;
    public UnitBlueprint firstTurret;
    public UnitBlueprint missileLauncher;
    public UnitBlueprint laserTurret;
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        buildManager.SelectUnitToBuild(standardTurret);
    }
    public void SelectFirstTurret()
    {
        buildManager.SelectUnitToBuild(firstTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectUnitToBuild(missileLauncher);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectUnitToBuild(laserTurret);
    }
}
