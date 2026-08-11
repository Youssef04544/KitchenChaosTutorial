using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private GameObject plateVisualPrefab;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    void Start()
    {
        platesCounter.onPlateSpawn += PlatesCounter_onPlateSpawn;
        platesCounter.onPlateRemove += PlatesCounter_onPlateRemove;
    }

    private void PlatesCounter_onPlateRemove(object sender, System.EventArgs e)
    {
        GameObject removedPlate = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(removedPlate);
        Destroy(removedPlate);

    }

    private void PlatesCounter_onPlateSpawn(object sender, System.EventArgs e)
    {
        float spawnOffsetY = 0.1f;
        GameObject spawnedPlate = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualGameObjectList.Add(spawnedPlate);
        spawnedPlate.transform.localPosition = new Vector3(0, spawnOffsetY * plateVisualGameObjectList.Count, 0);
    }


}
