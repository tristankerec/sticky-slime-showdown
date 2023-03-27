using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public SphereCollider spawnArea;
    public float spawnInterval = 10f;
    public GameObject coinPrefab;
    private GameObject lastSpawnedObject;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }
    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector3 randomSpherePoint = Random.insideUnitSphere * spawnArea.radius + spawnArea.transform.position;
            randomSpherePoint.y = 0.3f;

            RaycastHit hit;
            if (Physics.Raycast(randomSpherePoint, Vector3.down, out hit))
            {
                GameObject newObject = Instantiate(coinPrefab, hit.point + Vector3.up * 0.3f, Quaternion.identity);
                if (lastSpawnedObject != null)
                {
                    Destroy(lastSpawnedObject);
                }
                lastSpawnedObject = newObject;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
