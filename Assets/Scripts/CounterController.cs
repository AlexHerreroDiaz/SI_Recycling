using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
    public static CounterController Instance { get; private set; }

    public Text counterText;
    public Text DayText;

    private int counter;
    private int maxCounter; // Set the maximum counter value for each round
    private int currentRound;

    public RubbishManager rubbishManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetCounter();
        UpdateCounterText();
    }

    public void IncrementCounter()
    {
        counter++;
        UpdateCounterText();

        if (counter >= maxCounter)
        {
            if (rubbishManager != null)
            {
                rubbishManager.OnRoundCompleted();
                maxCounter = rubbishManager.GetMaxSpawnedRubbish();
            }
        }
    }

    public void ResetCounter()
    {
        counter = 0;
        maxCounter = rubbishManager.GetMaxSpawnedRubbish();
        UpdateCounterText();
        currentRound = rubbishManager.GetCurrentRound();
        UpdateDayText();
    }

    void UpdateCounterText()
    {
        counterText.text = counter + " / " + maxCounter;
    }
        void UpdateDayText()
    {
        DayText.text = "Day:" + currentRound;
    }
}
