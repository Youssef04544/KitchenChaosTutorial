using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        cuttingCounter.OnCuttingPerformed += CuttingCounter_OnCuttingPerformed;
    }

    private void CuttingCounter_OnCuttingPerformed(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

}
