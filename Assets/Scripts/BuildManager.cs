using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject buildEffectPrefab;
    public GameObject sellEffectPrefab;
    private TurretBlueprint turretToBuild;
    public TurretBlueprint TurretBlueprint { get { return turretToBuild; } }
    public bool CanBuild { get { return turretToBuild != null; } }

    public bool HasCurrency { get { return PlayerStats.Currency >= turretToBuild.cost; } }

    public NodeUI nodeUI;
    private Node selectedNode;

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
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
