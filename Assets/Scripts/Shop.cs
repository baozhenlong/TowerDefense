using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    private BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void OnClickStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void OnClickMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }
}
