using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesOnGameObject;
    [SerializeField] private StoveCounter stoveCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventArgs e)
    {
        bool showVisuals = e.state == StoveCounter.State.Frying;
        stoveOnGameObject.SetActive(showVisuals);
        particlesOnGameObject.SetActive(showVisuals);

    }

}
