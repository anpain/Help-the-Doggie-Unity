using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public float speed;
    public float radius;

    [Space(10)]
    public LayerMask layer;

    [Space(10)]
    public CircleCollider2D col;
    public Rigidbody2D rb;

    [Space(10)]
    public GameObject target;

    float lastKnock = 0;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Dog");
    }

    void FixedUpdate()
    {
        lastKnock = lastKnock + Time.deltaTime;

        if (!target.GetComponent<DogIsCatch>().isHitted)
        {
            Vector2 _dir = (target.transform.position - transform.position);
            _dir.Normalize();
            float angle = Mathf.Atan2(-_dir.x, _dir.y) * Mathf.Rad2Deg;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        }

        RaycastHit2D hit = Physics2D.CircleCast(col.bounds.center, radius, Vector2.left);

        if (hit.collider.IsTouchingLayers(layer) && lastKnock >= 1f && !target.GetComponent<DogIsCatch>().isHitted)
        {
            lastKnock = 0;
            StartCoroutine("SpeedX");
        }

    }

    IEnumerator SpeedX()
    {
        rb.AddForce(transform.up * 3f, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.5f);
        speed = -(speed * 0.75f);

        int i = Random.Range(1, 3);
        if (i == 1)
            rb.AddForce(transform.right * -10f, ForceMode2D.Impulse);
        else
            rb.AddForce(-transform.right * -10f, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.2f);
        speed = -(speed * 1.33f);

        StopCoroutine("SpeedX");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

}

