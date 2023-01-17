using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_Draw : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;
    [SerializeField] LineRenderer line;
    [SerializeField] EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    [SerializeField] float drawingAreaMaxX = 0f;
    [SerializeField] float drawingAreaMinX = 0f;
    [SerializeField] float drawingAreaMaxY = 0f;
    [SerializeField] float drawingAreaMinY = 0f;

    [SerializeField] Camera mainCam = null;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        // Debug.Log("ÁÂÇ¥ : " + mainCam.ScreenToWorldPoint(Input.mousePosition));
        if (CheckArea() == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject Obj = Instantiate(linePrefab);
                line = Obj.GetComponent<LineRenderer>();
                col = Obj.GetComponent<EdgeCollider2D>();
                Obj.transform.position = Vector3.zero;
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                line.positionCount = 1;
                line.SetPosition(0, points[0]);
            }

            else if (Input.GetMouseButton(0))
            {
                Vector2 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);

                points.Add(pos);
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, pos);
                col.points = points.ToArray();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                points.Clear();
            } 
        }
    }

    private bool CheckArea()
    {
        Vector3 curTouchPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (curTouchPos.x < drawingAreaMaxX && curTouchPos.y < drawingAreaMaxY &&
            curTouchPos.x > drawingAreaMinX && curTouchPos.y > drawingAreaMinY)
            return true;
        else
            return false;
    }
}
