using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public SphereCollider spawnArea;
    public float spawnInterval = 20f;
    public GameObject speedPowerUpPrefab;
    private GameObject lastSpawnedObject;

    private void Start()
    {
        StartCoroutine(SpawnObjectsAfterDelay());
    }

    private IEnumerator SpawnObjectsAfterDelay()
    {
        float randomTime = Random.Range(20f, 25f);
        yield return new WaitForSeconds(randomTime);
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
                GameObject newObject = Instantiate(speedPowerUpPrefab, hit.point + Vector3.up * 0.3f, Quaternion.identity);
                newObject.AddComponent<powerUpSpin>();

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
