using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private NPC[] npcs;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnArea = 120;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea, spawnArea), 0, Random.Range(-spawnArea, spawnArea));
            Instantiate(npcs[Random.Range(0, npcs.Length)], spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
