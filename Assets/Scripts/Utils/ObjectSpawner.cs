using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {
    public enum Type {
        Target, Evade, Powerup
    }

    [Tooltip("Object in the scene that the instantiated objects will be parented to.")]
    public GameObject container;
    [Tooltip("Objects to be instantiated.")]
    public List<GameObject> prefabs;
    [Tooltip("Minimum time in seconds before instantiating.")]
    public float minTime;
    [Tooltip("Maximum time in seconds before instantiating.")]
    public float maxTime;
    public Type type;

    private Collider2D spawnArea;
    private Renderer prefabRenderer;
    //private float currentTime, nextTime;

    void Awake() {
        spawnArea = container.GetComponent<Collider2D>();
    }

    void Start() {
        Invoke("InstantiateRepeating", GetRandomTime());
    }

    private float GetRandomTime() {
        return Random.Range(minTime, maxTime);
    }

    private GameObject GetRandomPrefab() {
        var r = Random.Range(0, prefabs.Count);
        return prefabs[r];
    }

    private Vector2 GetRandomPosition() {
        float x = Random.Range(
            -spawnArea.bounds.size.x + prefabRenderer.bounds.size.x,
            spawnArea.bounds.size.x - prefabRenderer.bounds.size.x
        );
        float y = Random.Range(
            -spawnArea.bounds.size.y + prefabRenderer.bounds.size.y,
            spawnArea.bounds.size.y - prefabRenderer.bounds.size.y
        );
        return new Vector2(x, y);
    }

    private GameObject InstantiatePrefab() {
        GameObject go;
        var selectedPrefab = GetRandomPrefab();
        //if it's too slow, we can store the renderers in another list on Awake
        prefabRenderer = selectedPrefab.GetComponentInChildren<Renderer>();
        go = (GameObject)Instantiate(selectedPrefab, GetRandomPosition(), Quaternion.identity);
        go.transform.SetParent(container.transform);
        return go;
    }

    private void InstantiateRepeating() {
        InstantiatePrefab();
        Invoke("InstantiateRepeating", GetRandomTime());
    }

    void OnDestroy() {
        Destroy(container);
    }

}
