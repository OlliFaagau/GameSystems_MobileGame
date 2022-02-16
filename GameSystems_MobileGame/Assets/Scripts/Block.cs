using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }
}
