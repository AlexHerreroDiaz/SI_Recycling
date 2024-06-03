using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
    public static CounterController Instance { get; private set; }

    public Text counterText; // Assign this in the Inspector
    private int counter;
    private int maxCounter; // Set the maximum counter value for each round

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
    }

    void UpdateCounterText()
    {
        counterText.text = counter + " / " + maxCounter;
    }
}
