﻿using System;
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
        if (renderer.positionCount == 0)//allow line to grow
            return true;

        if (renderer.positionCount > 20)//stop appending line at certain length
            return false;

        return Vector2.Distance(renderer.GetPosition(renderer.positionCount - 1), pos)  > LineScript.RESOLUTION;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Block"))//if obstacles collide with line -> destroy obstacle
        {
            Destroy(col.gameObject);
        }
    }

}
