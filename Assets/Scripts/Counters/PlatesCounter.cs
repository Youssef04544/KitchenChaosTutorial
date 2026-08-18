using System;
using UnityEngine;


public class PlatesCounter : BaseCounter
{
    public event EventHandler onPlateSpawn;
    public event EventHandler onPlateRemove;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer = 0f;
    private float spawnPlateTimerMax = 3f;
    private int platesSpawnedAmountMax = 4;
    private int platesSpawnedAmount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (platesSpawnedAmount < platesSpawnedAmountMax)
        {
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnPlateTimerMax && GameManager.Instance.IsGamePlaying())
            {
                spawnPlateTimer = 0f;
                platesSpawnedAmount++;
                onPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }

    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnedAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                platesSpawnedAmount--;
                onPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
