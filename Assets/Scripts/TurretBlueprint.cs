using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public Vector3 positionOffset;

    public int cost;
    public GameObject upgradedPrefab;
    public int upgradedCost;
    public int Worth { get { return cost / 2; } }
}
