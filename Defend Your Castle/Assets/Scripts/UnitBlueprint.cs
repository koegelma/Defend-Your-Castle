using UnityEngine;
[System.Serializable]
public class UnitBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;
    public float unitRadius;

    public float GetUnitRadius()
    {
        unitRadius = prefab.GetComponent<UnitBehaviour>().range / 2;
        return unitRadius;
    }

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
