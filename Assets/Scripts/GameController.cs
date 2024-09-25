using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{
    public int maxPoints;
    public float timer;

    [Space(10)]
    public bool isStarting;

    [Space(10)]
    public LineDrawer LineDrawer;
    public Image statusBar;
    public TextMeshProUGUI timerText;

    [Space(10)]
    public GameObject resultGO;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private bool starsAreGiven = false;


    private void FixedUpdate()
    {
        if (isStarting && timer >= -0.5 && LineDrawer.lineIsDrawn)
        {
            timer -= Time.deltaTime;
            timerText.text = (timer + 1).ToString("F0");
        }
        else if (LineDrawer.lineIsDrawn)
        {
            timerText.text = "0";
            isStarting = false;
        }

        if (statusBar.fillAmount >= 0.001)
            statusBar.fillAmount = (1 - (float)LineDrawer.pCount / (float)maxPoints);

        if (!isStarting && LineDrawer.lineIsDrawn && !starsAreGiven)
        {
            GetStar();
            starsAreGiven = true;
        }
    }

    private void GetStar()
    {
        resultGO.SetActive(true);

        if (statusBar.fillAmount >= 0.67)
            Get3Star();
        else if (statusBar.fillAmount >= 0.34)
            Get2Star();
        else
            Get1Star();
    }

    private void Get3Star()
    {
        star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(true);
    }

    private void Get2Star()
    {
        star1.SetActive(true);
        star2.SetActive(true);
    }

    private void Get1Star()
    {
        star1.SetActive(true);
    }

}
