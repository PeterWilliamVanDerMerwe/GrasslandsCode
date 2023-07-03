using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isLevelStarted;

    [SerializeField] private bool isLevelComplete;
    [SerializeField] private float countdownTimer;
    [SerializeField] private TextMeshProUGUI txtCountdownTime;

    private SheepSpawner spawner;
    private Pen pen;

    void Start()
    {
        isLevelComplete = false;
        isLevelStarted = false;
        StartCoroutine(LevelCountdown());

        spawner = FindObjectOfType<SheepSpawner>();
        pen = FindObjectOfType<Pen>();
    }

    private void Update()
    {
        LevelComplete();
    }

    /// <summary>
    /// This method is the win condition for each level and will complete a level when the number of sheep
    /// that have been spawned match the number of sheep that have entered the pen.
    /// </summary>
    private void LevelComplete()
    {
        if (isLevelStarted)
        {
            if (pen.sheepCount == spawner.numOfSheepToSpawn)
            {
                Time.timeScale = 0.0f;
                isLevelComplete = true;
            }
        }
    }

    /// <summary>
    /// This coroutine is used as the countdown timer at the start of each level before sheep start spawning.
    /// </summary>
    IEnumerator LevelCountdown()
    {
        while (countdownTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            countdownTimer -= 1f;
            txtCountdownTime.text = "Time Left: " + countdownTimer;
        }
        spawner.SpawnSheep();
        isLevelStarted = true;
    }
}
