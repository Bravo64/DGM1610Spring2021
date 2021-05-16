using UnityEngine;
using UnityEngine.Events;

public class ScoreReachedEventBehaviour : MonoBehaviour
{
    public IntData score;
    public int desiredScore;
    public UnityEvent scoreReachedEvent;
    
    public void CheckScore()
    {
        if (score.value >= desiredScore)
        {
            scoreReachedEvent.Invoke();
        }
    }
}
