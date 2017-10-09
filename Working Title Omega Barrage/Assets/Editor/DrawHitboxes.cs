using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hitbox))]
public class DrawHitboxes : Editor
{
    Hitbox boxToDraw;

    void OnEnable()
    {
        boxToDraw = (Hitbox)target;
    }

    void OnSceneGUI()
    {
        DrawAllBoxes(boxToDraw.GatherChildBoxes());
    }

    void DrawAllBoxes(List<AxisAlignedBoundingBox> boxList)
    {
        foreach (AxisAlignedBoundingBox box in boxList)
        {
            if (box.isActiveAndEnabled)
            {
                switch (box.gameObject.layer)
                {
                    case 0:
                        Handles.color = Color.white;
                        break;
                    case 8:
                        Handles.color = Color.black;
                        break;
                    case 9:
                        Handles.color = Color.cyan;
                        break;
                    case 10:
                        if (box.gameObject.GetComponent<Hitbox>() == box)
                        {
                            switch (box.gameObject.GetComponent<Hitbox>().hitboxID)
                            {
                                case 1:
                                    Handles.color = Color.red;
                                    break;
                                case 2:
                                    Handles.color = Color.red;
                                    break;
                                case 3:
                                    Handles.color = Color.red;
                                    break;
                                case 4:
                                    Handles.color = Color.red;
                                    break;
                                case 5:
                                    Handles.color = Color.yellow;
                                    break;
                                default:
                                    Handles.color = Color.magenta;
                                    break;
                            }
                        }
                        break;
                    default:
                        Handles.color = Color.white;
                        break;
                }
                DrawBounds(box);
            }
        }
    }


    void DrawBounds(AxisAlignedBoundingBox boxToDraw)
    {
        Vector3 topLeftCorner = new Vector3(boxToDraw.transform.position.x - boxToDraw.halfSize.x, boxToDraw.transform.position.y + boxToDraw.halfSize.y, 0f);
        Vector3 topRightCorner = new Vector3(boxToDraw.transform.position.x + boxToDraw.halfSize.x, boxToDraw.transform.position.y + boxToDraw.halfSize.y, 0f);
        Vector3 bottomLeftCorner = new Vector3(boxToDraw.transform.position.x - boxToDraw.halfSize.x, boxToDraw.transform.position.y - boxToDraw.halfSize.y, 0f);
        Vector3 bottomRightCorner = new Vector3(boxToDraw.transform.position.x + boxToDraw.halfSize.x, boxToDraw.transform.position.y - boxToDraw.halfSize.y, 0f);



        Handles.DrawLine(topLeftCorner, topRightCorner);
        Handles.DrawLine(topRightCorner, bottomRightCorner);
        Handles.DrawLine(bottomRightCorner, bottomLeftCorner);
        Handles.DrawLine(bottomLeftCorner, topLeftCorner);
    }
}
