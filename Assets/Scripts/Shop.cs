using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    private BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void OnClickStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
}
