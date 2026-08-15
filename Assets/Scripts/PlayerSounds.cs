using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepsTimer;
    private float footstepsTimerMax = .15f;
    private float footstepsSoundVolume = 1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        footstepsTimer -= Time.deltaTime;
        if (footstepsTimer <= 0)
        {
            footstepsTimer = footstepsTimerMax;
            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(transform.position, footstepsSoundVolume);
            }
        }
    }
}
