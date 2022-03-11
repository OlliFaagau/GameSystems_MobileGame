using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField]public LineRenderer renderer;
    [SerializeField]public EdgeCollider2D collider;

    private readonly List<Vector2> points =  new List<Vector2>();

    void Start()
    {
        collider.transform.position -= transform.position;
    }

    void Update()
    {
        Debug.Log(renderer.positionCount);
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
            return;

        points.Add(pos);
        renderer.positionCount++;
        renderer.SetPosition(renderer.positionCount - 1, pos);

        collider.points = points.ToArray();
    }

    private bool CanAppend(Vector2 pos)
    {
        if (renderer.positionCount == 0)
            return true;

        if (renderer.positionCount > 20)
            return false;

        return Vector2.Distance(renderer.GetPosition(renderer.positionCount - 1), pos)  > LineScript.RESOLUTION;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hit");
        if (col.gameObject.CompareTag("Block"))
        {
            Destroy(col.gameObject);
        }
    }

}
