using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public event EventHandler OnVolumeChanged;
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        Player.Instance.OnObjectPickup += Player_OnObjectPickup;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        BaseCounter.OnObjectDropped += BaseCounter_OnObjectDropped;
        PlateKitchenObject.OnIngredientPickup += PlateKitchenObject_OnIngredientPickup;
    }

    private void PlateKitchenObject_OnIngredientPickup(object sender, System.EventArgs e)
    {
        PlateKitchenObject plateKitchenObject = sender as PlateKitchenObject;
        PlaySound(audioClipRefsSO.ObjectPickup, plateKitchenObject.transform.position);
    }

    private void BaseCounter_OnObjectDropped(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void Player_OnObjectPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.ObjectPickup, Player.Instance.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume >= 1.1f)
        {
            volume = 0f;
        }
        OnVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
    public float GetVolume()
    {
        return volume;
    }
}
