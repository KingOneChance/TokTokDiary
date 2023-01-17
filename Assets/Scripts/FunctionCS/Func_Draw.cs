using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeDraw
{
    public class Func_Draw : MonoBehaviour
    {
        [SerializeField] GameObject linePrefab;
        [SerializeField] public LineRenderer line = null;
        public Color curColor = Color.black;
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
            line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (CheckArea() == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject Obj = Instantiate(linePrefab);
                    line = Obj.GetComponent<LineRenderer>();
                    col = Obj.GetComponent<EdgeCollider2D>();
                    Obj.transform.position = Vector3.zero;
                    Obj.GetComponent<LineRenderer>().startColor = curColor;
                    Obj.GetComponent<LineRenderer>().endColor = curColor;

                    points.Add(mainCam.ScreenToWorldPoint(Input.mousePosition));
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
            {

                return true;
            }
            else
                return false;
        }

        public void selectedColor(ColorType type)
        {
            switch (type)
            {
                case ColorType.Red :
                    curColor = Color.red;
                    break;
                case ColorType.Orange:
                    curColor = Color.red;
                    break;
                case ColorType.Yellow:
                    curColor = Color.yellow;
                    break;
                case ColorType.Green:
                    curColor = Color.green;
                    break;
                case ColorType.Blue:
                    curColor = Color.blue;
                    break;
                case ColorType.Purple:
                    curColor = Color.blue;
                    break;
                case ColorType.Pink:
                    curColor = Color.red;
                    break;
                case ColorType.Brown:
                    curColor = Color.yellow;
                    break;
                case ColorType.Black:
                    curColor = Color.black;
                    break;
            }
        }
    }
}
