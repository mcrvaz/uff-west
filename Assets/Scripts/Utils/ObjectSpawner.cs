using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {
    [Tooltip("Objects to be instantiated.")]
    public List<GameObject> prefabs;
    [Tooltip("Minimum time in seconds before instantiating.")]
    public float minTime;
    [Tooltip("Maximum time in seconds before instantiating.")]
    public float maxTime;

    private Collider2D spawnArea;
    private Renderer prefabRenderer;
    private float currentTime, nextTime;

    void Awake() {
        spawnArea = GetComponent<Collider2D>();
    }

    void Start() {
        Invoke("InstantiateRepeating", GetRandomTime());
    }

    private float GetRandomTime() {
        return Random.Range(minTime, maxTime);
    }

    private Vector2 GetRandomPosition() {
        float randomX = Random.Range(
            -spawnArea.bounds.size.x + prefabRenderer.bounds.size.x,
            spawnArea.bounds.size.x - prefabRenderer.bounds.size.x
        );
        float randomY = Random.Range(
            -spawnArea.bounds.size.y + prefabRenderer.bounds.size.y,
            spawnArea.bounds.size.y - prefabRenderer.bounds.size.y
        );
        return new Vector2(randomX, randomY);
    }

    private GameObject InstantiatePrefab() {
        var selectedPrefab = prefabs[Random.Range(0, prefabs.Count)];
        //if it's too slow, we can store the renderers in another list on Awake
        prefabRenderer = selectedPrefab.GetComponentInChildren<Renderer>();
        return (GameObject)Instantiate(selectedPrefab, GetRandomPosition(), Quaternion.identity);
    }

    private void InstantiateRepeating() {
        InstantiatePrefab();
        Invoke("InstantiateRepeating", GetRandomTime());
    }

}
