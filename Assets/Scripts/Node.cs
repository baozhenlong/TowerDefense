using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private GameObject turret;
    private TurretBlueprint turretBlueprint;
    private Renderer rendererComp;
    private Color startColor;

    public Vector3 BuildPosition { get { return transform.position + turretBlueprint.positionOffset; } }
    private BuildManager buildManager;

    private void Awake()
    {
        rendererComp = GetComponent<Renderer>();
    }

    private void Start()
    {
        buildManager = BuildManager.instance;
        startColor = rendererComp.material.color;
    }

    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        rendererComp.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rendererComp.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Already has turret");
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        BuildTurret(buildManager.TurretBlueprint);
    }

    private void BuildTurret(TurretBlueprint _turretBlueprint)
    {
        turretBlueprint = _turretBlueprint;
        turret = Instantiate(turretBlueprint.prefab, BuildPosition, Quaternion.identity);
        Debug.Log("turret build!");
    }
}
