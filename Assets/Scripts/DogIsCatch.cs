using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIsCatch : MonoBehaviour
{
    public LayerMask layer;
    public CircleCollider2D col;
    public float radius;
    public bool isHitted;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(col.bounds.center, radius, Vector2.left);
        if (hit.collider.tag == "Bee")
        {
            isHitted = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(col.bounds.center, radius);
    }


}
