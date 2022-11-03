using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject mouseVisual;
    public LayerMask groundLayerMask;
    public LayerMask unitLayerMask;

    [SerializeField] private List<GameObject> SelectedUnits = new List<GameObject>();
    [SerializeField] private Vector3 dragStartPos;

    private Vector3 offset;
    private Vector3 size;

    void Update()
    {
        // Update mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit mousePos, 999f, groundLayerMask))
        {
            mouseVisual.transform.position = mousePos.point;

            // Click and drag
            if (Input.GetMouseButtonDown(0))
            {
                dragStartPos = mousePos.point;
            }

            if (Input.GetMouseButtonUp(0))
            {
                DeselectUnits();
                DragSelect(mousePos.point);
            }
        }

        // Mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickray, out RaycastHit unitRaycastHit, 999f, unitLayerMask))
            {
                // Select a unit if clicked
                if (unitRaycastHit.collider.gameObject.tag == "controlled")
                {
                    Debug.Log("Selected Unit");
                    UnitSelect(unitRaycastHit.collider.gameObject);
                }
            } 
        }

        // Move units
        if (Input.GetMouseButtonDown(1))
        {
            Ray clickray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickray, out RaycastHit groundRaycastHit, 999f, groundLayerMask))
            {
                if (SelectedUnits.Count > 0)
                {
                    MoveUnits(groundRaycastHit.point);
                }
            }
        }
    }

    public void UnitSelect(GameObject unit)
    {
        DeselectUnits();
        SelectedUnits.Add(unit);
    }

    public void DragSelect(Vector3 endPos)
    {
        var width = endPos.x - dragStartPos.x;
        var height = endPos.z - dragStartPos.z;
        offset = dragStartPos + new Vector3(width / 2, 0, height / 2);
        size = new Vector3(Mathf.Abs(width), 10, Mathf.Abs(height));

        Collider[] cols = Physics.OverlapBox(offset, size/2);

        foreach(Collider col in cols)
        {
            if (col.gameObject.tag == "controlled")
            {
                SelectedUnits.Add(col.gameObject);
                col.gameObject.GetComponent<UnitControl>().ToggleSelected(true);
            }
        }
    }

    public void DeselectUnits()
    {
        foreach (GameObject unit in SelectedUnits)
        {
            unit.GetComponent<UnitControl>().ToggleSelected(false);
        }
        SelectedUnits.Clear();
    }

    public void MoveUnits(Vector3 pos)
    {
        Debug.Log("Moving Units");
        var positions = GetUnitPositions(pos, SelectedUnits.Count);
        int count = 0;
        foreach (GameObject unit in SelectedUnits.ToArray())
        {
            if (unit != null)
                unit.GetComponent<UnitClass>().MoveTo(positions[count]);
            else
                SelectedUnits.Remove(unit);

            count++;
        }
    }

    private List<Vector3> GetUnitPositions(Vector3 pos, int numUnits)
    {
        List<Vector3> positions = new List<Vector3>();

        var rows = Mathf.CeilToInt(Mathf.Sqrt(numUnits));
        for (int x = 0; x < rows; x++)
        {
            var zpos = pos.z + (x - (rows/2));

            for (int z = 0; z < rows; z++)
            {
                var xpos = pos.x + (z - (rows/2));

                positions.Add(new Vector3(xpos, 1, zpos));

                if(positions.Count == numUnits)
                {
                    x = rows;
                    z = rows;
                }
            }
        }

        return positions;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(offset, size);
    }
}
