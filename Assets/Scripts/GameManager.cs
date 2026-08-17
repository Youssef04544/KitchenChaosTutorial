using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnPaused;
    public event EventHandler OnUnpaused;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 15f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        gamePlayingTimer = gamePlayingTimerMax;
    }

    private void Start()
    {
        GameInput.Instance.OnPause += GameInput_OnPause;
    }

    private void GameInput_OnPause(object sender, EventArgs e)
    {
        TogglePause();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                }
                break;
            case State.GamePlaying:

                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return (state == State.GamePlaying);
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            OnPaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            OnUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}

