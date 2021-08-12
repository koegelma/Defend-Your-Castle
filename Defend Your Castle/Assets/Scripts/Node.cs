using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color insufficientMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject unit;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if (!buildManager.CanBuild) { return; }

        if (unit != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        buildManager.BuildUnitOn(this);
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if (!buildManager.CanBuild) { return; }

        if (buildManager.HasMoney) { rend.material.color = hoverColor; }
        else { rend.material.color = insufficientMoneyColor; }
    }
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
