using UnityEditor.Search;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject selectedVisualGameObject;
    
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedClearCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        selectedVisualGameObject.SetActive(true);
    }
    private void Hide()
    {
        selectedVisualGameObject.SetActive(false);
    }
}
