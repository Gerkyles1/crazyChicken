using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int startTime;
    private static bool itIsReplay = false;
    [SerializeField] private GameObject meinMenu;
    [SerializeField] private GameObject replayGroup;
    [SerializeField] private Text scoreText;
    void Start()
    {
        if (itIsReplay)
        {
            itIsReplay = false;
            startPlay();
        }
        else
        {
            Time.timeScale = 0;
            startTime = (int)Time.time;
        }
        PlayerMovement.playerDie += showScore;
    }

    public void startPlay()
    {
        meinMenu.SetActive(false);
        replayGroup.SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);

    }

    public void replay()
    {
        itIsReplay = true;
        reloadScene();
    }
    private void showScore()
    {
        scoreText.text = "GAME OVER\n\nYOUR SCORE\n " + (int)(Time.time - startTime) * 7 + "m";
        gameObject.SetActive(true);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void OnDestroy()
    {
        PlayerMovement.playerDie -= showScore;
    }


}
