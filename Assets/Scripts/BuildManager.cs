using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TurretBlueprint turretToBuild;
    public TurretBlueprint TurretBlueprint { get { return turretToBuild; } }
    public bool CanBuild { get { return turretToBuild != null; } }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"More than one BuildManager in scene at node({transform.name})!");
            return;
        }
        instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint _turretToBuild)
    {
        turretToBuild = _turretToBuild;
    }
}