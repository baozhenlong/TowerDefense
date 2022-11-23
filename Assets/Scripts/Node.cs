using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughCurrencyColor;
    private GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        if (buildManager.HasCurrency)
        {
            rendererComp.material.color = hoverColor;
        }
        else
        {
            rendererComp.material.color = notEnoughCurrencyColor;
        }
    }

    private void OnMouseExit()
    {
        rendererComp.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManager.SelectNode(this);
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
        if (PlayerStats.Currency < _turretBlueprint.cost)
        {
            Debug.Log("Not enough money to build!");
            return;
        }

        PlayerStats.Currency -= _turretBlueprint.cost;

        turretBlueprint = _turretBlueprint;
        GameObject buildEffect = Instantiate(buildManager.buildEffectPrefab, BuildPosition, Quaternion.identity);
        Destroy(buildEffect, buildEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main.startLifetime.constant + 3f);
        turret = Instantiate(turretBlueprint.prefab, BuildPosition, Quaternion.identity);
        Debug.Log("turret build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Currency < turretBlueprint.upgradedCost)
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }
        isUpgraded = true;
        PlayerStats.Currency -= turretBlueprint.upgradedCost;
        Destroy(turret);
        GameObject buildEffect = Instantiate(buildManager.buildEffectPrefab, BuildPosition, Quaternion.identity);
        Destroy(buildEffect, buildEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main.startLifetime.constant + 3f);
        turret = Instantiate(turretBlueprint.upgradedPrefab, BuildPosition, Quaternion.identity);
        Debug.Log("turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Currency += turretBlueprint.Worth;
        GameObject sellEffect = Instantiate(buildManager.sellEffectPrefab, BuildPosition, Quaternion.identity);
        Destroy(sellEffect, sellEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main.startLifetime.constant + 3f);
        Destroy(turret);
        turret = null;
        turretBlueprint = null;
    }
}
