using System.Collections;
using UnityEngine;

public class BeeSpawn : MonoBehaviour
{
    public bool isEnabled = false;
    public bool wasSpawned = false;
    public int beeCount = 5;

    [Space(10)]
    public GameObject bee;

    void Update()
    {
        if (isEnabled && !wasSpawned)
            StartCoroutine("Spawn");
    }
   
    IEnumerator Spawn()
    {
        isEnabled = false;
        wasSpawned = true;
        for (int i = 0; i < beeCount; i++)
        {
            GameObject newBee = Instantiate(bee);
            newBee.transform.SetParent(this.transform);
            newBee.transform.localPosition = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
            newBee.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            yield return new WaitForSeconds(0.3f);
        }

        StopCoroutine("Spawn");
    }
}
