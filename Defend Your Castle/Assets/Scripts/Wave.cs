using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemy;
    public int count;
    public float rate;
    public bool hasEnemyVariation;
    public GameObject enemyVariation; //TODO: create Custom Editor so object + int variable shows only if bool is true
    public int variationModulo;
}


