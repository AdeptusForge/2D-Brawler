using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AxisAlignedBoundingBox : MonoBehaviour{


    public List<LayerMask> collidesWith;

    public Vector2 center;
    public Vector2 offset;
    public Vector2 halfSize;

    public List<AxisAlignedBoundingBox> prevOverlap;
    public List<AxisAlignedBoundingBox> currOverlap;

    //public AABB(Vector2 center, Vector2 halfSize)
    //{
    //    this.center = center;
    //    this.halfSize = halfSize;
    //}

    public bool Overlaps(AxisAlignedBoundingBox other)
    {
        if (collidesWith.Contains(other.gameObject.layer))
        {
            if (Mathf.Abs(center.x - other.center.x) > halfSize.x + other.halfSize.x) return false;
            if (Mathf.Abs(center.y - other.center.y) > halfSize.y + other.halfSize.y) return false;

            Debug.Log("Overlap");
            return true;
        }
        else
        {
            return false;
        }
    }



    public List<AxisAlignedBoundingBox> GatherChildBoxes()
    {
        List<AxisAlignedBoundingBox> list = new List<AxisAlignedBoundingBox>();
        list.AddRange(GetComponentsInChildren<AxisAlignedBoundingBox>());
        list.AddRange(GetComponentsInChildren<Hitbox>());
        return list;
    }


    // Use this for initialization
    void Awake () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        center = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);

    }



}
