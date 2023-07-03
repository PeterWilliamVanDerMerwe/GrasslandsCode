using System.Collections;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public int numOfSheepToSpawn;

    [SerializeField] private int spawnedSheep;
    [SerializeField] private float SheepDelay;
    [SerializeField] private GameObject Sheep;
    [SerializeField] private LineRenderer DottedLine;

    void Start()
    {
        DottedLine.enabled = false;
    }

    public void SpawnSheep()
    {
        StartCoroutine(SpawnDelay());
    }

    /// <summary>
    /// A coroutine that will spawn sheep continuously until the number of spawned sheep is greater than the number of sheep you would like spawned.
    /// Sheep will be spawned with a delay in seconds dictated by the SheepDelay float variable.
    /// </summary>
    IEnumerator SpawnDelay()
    {
        while (spawnedSheep < numOfSheepToSpawn)
        {
            yield return new WaitForSeconds(SheepDelay);
            Vector3 spawnPosition = transform.position + transform.forward;
            Instantiate(Sheep, spawnPosition, transform.rotation);
            spawnedSheep++;
        }
    }
}
