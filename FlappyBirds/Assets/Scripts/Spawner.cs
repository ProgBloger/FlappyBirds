using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] float minHeight = -1f;
    [SerializeField] float maxHeight = 1f;

    public void StopSpawn()
    {
        OnDisable();
    }

    public void StartSpawn()
    {
        OnEnable();
    }

    public void BlowUpPipes()
    {
        IEnumerable<Pipes> pipes = FindObjectsOfType<Pipes>();

        foreach (var pipeSet in pipes)
        {
            Destroy(pipeSet.gameObject);
        }
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
