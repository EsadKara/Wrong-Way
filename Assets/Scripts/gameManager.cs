using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    [SerializeField] GameObject gameOverPnl, pausePnl, magnetPnl, magnet, boots, bootsPnl, settingsPnl, coins, rocket, rocketPnl;
    [SerializeField] Text timeTxt, coinTxt, scoreTxt, endTimeTxt, endScoreTxt, highScoreTxt, endHighScoreTxt;
    [SerializeField] AudioClip coinSound, collectiblesSound;
    [SerializeField] Image magnetImg, bootsImg, rocketImg;
    [SerializeField] GameObject player;

    playerControl playerCs;
    AudioSource audioSource, audioSourcePlayer;

    public bool hasMagnet, hasBoots;

    float second, minute, score, highScore, magnetTime, bootsTime;
    int coinCount;


    void Start()
    {
        Time.timeScale = 1;

        playerCs = player.GetComponent<playerControl>();
        audioSource = GetComponent<AudioSource>();
        audioSourcePlayer = player.GetComponent<AudioSource>();
        audioSourcePlayer.Play();

        gameOverPnl.SetActive(false);
        magnetPnl.SetActive(false);
        bootsPnl.SetActive(false);
        pausePnl.SetActive(false);
        settingsPnl.SetActive(false);
        rocketPnl.SetActive(false);

        InvokeRepeating("CarSpawn", 0f,2.5f);
        InvokeRepeating("CollectiblesSpawn", 0f, 7f);

        minute = 0;
        second = 0;
        coinCount = 0;
        score = 0;
        magnetTime = 10f;
        bootsTime = 10f;

        hasMagnet = false;
        hasBoots = false;

        coinTxt.text = "x " + coinCount;
        scoreTxt.text = "= " + score;


        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetFloat("HighScore");
        else
            highScore = 0;
    }

    
    void Update()
    {
        Timer();
        score += 50 * Time.deltaTime;
        scoreTxt.text = "= " + (int)score;
        highScoreTxt.text = "= " + (int)highScore;

        if (hasMagnet)
        {
            magnetTime -= Time.deltaTime;
            magnetImg.rectTransform.localScale = new Vector3(magnetTime / 10, 1, 1);
            if (magnetTime <= 0)
            {
                hasMagnet = false;
                magnetPnl.SetActive(false);
                magnetTime = 10;
            }
        }
        if (hasBoots)
        {
            bootsTime -= Time.deltaTime;
            bootsImg.rectTransform.localScale = new Vector3(bootsTime / 10, 1, 1);
            if (bootsTime <= 0)
            {
                hasBoots = false;
                bootsPnl.SetActive(false);
                bootsTime = 10;
            }
        }
        if (playerCs.hasRocket)
            rocketImg.rectTransform.localScale = new Vector3(playerCs.rocketTime / 10, 1, 1);
        else
            rocketPnl.SetActive(false);
    }

    void CarSpawn()
    {
        int carNumber = Random.Range(0, 6);
        int carPos = Random.Range(0, 2);
        if (carPos == 0)
        {
            Instantiate(cars[carNumber], new Vector3(player.transform.position.x + 40, 0.05f, 0.625f),cars[carNumber].transform.rotation);
        }
        else
        {
            Instantiate(cars[carNumber], new Vector3(player.transform.position.x + 40, 0.05f, -0.625f), cars[carNumber].transform.rotation);
        }
    }

    void CollectiblesSpawn()
    {
        int random = Random.Range(0, 8);
        if (random <= 2) 
        {
            int random2 = Random.Range(0, 2);
            if (random2 == 0)
            {
                Instantiate(magnet, new Vector3(player.transform.position.x + 40, 0.5f, 0.78f), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(magnet, new Vector3(player.transform.position.x + 40, 0.5f, -0.47f), Quaternion.Euler(0, 0, 0));
            }

        }
        else if (random <= 5) 
        {
            int random2 = Random.Range(0, 2);
            if (random2 == 0)
            {
                Instantiate(boots, new Vector3(player.transform.position.x + 40, 0.5f, 0.55f), Quaternion.Euler(0, -40, 0));
            }
            else
            {
                Instantiate(boots, new Vector3(player.transform.position.x + 40, 0.5f, -0.65f), Quaternion.Euler(0, -40, 0));
            }
        }
        else
        {
            int random2 = Random.Range(0, 2);
            if (random2 == 0)
            {
                Instantiate(rocket, new Vector3(player.transform.position.x + 40, 0.4f, 0.625f), Quaternion.Euler(0, -40, 0));
            }
            else
            {
                Instantiate(rocket, new Vector3(player.transform.position.x + 40, 0.4f, -0.625f), Quaternion.Euler(0, -40, 0));
            }
        }
    }

    void CoinSpawn()
    {
        int xPos = 6;
        for(int i = 0; i < 6; i++)
        {
            int coinPos = Random.Range(0, 3);
            Vector3 spawnPos;
            if (coinPos == 0)
            {
                spawnPos = new Vector3(player.transform.position.x + xPos, 2.2f, 0.625f);
            }
            else if (coinPos == 1)
            {
                spawnPos = new Vector3(player.transform.position.x + xPos, 2.2f, 0);
            }
            else
            {
                spawnPos = new Vector3(player.transform.position.x + xPos, 2.2f, -0.625f);
            }
            Instantiate(coins, spawnPos, coins.transform.rotation);
            xPos += 6;
        }
    }

    public void GameOver()
    {
        highScore = PlayerPrefs.GetFloat("HighScore");
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        Time.timeScale = 0;
        endTimeTxt.text = timeTxt.text;
        endScoreTxt.text = " "+(int)score;
        endHighScoreTxt.text = " "+ (int)highScore;
        gameOverPnl.SetActive(true);
    }

    void Timer()
    {
        second += Time.deltaTime;
        if (second > 59)
        {
            second = 0;
            minute++;
        }
        if (minute < 10)
        {
            if (second < 10)
            {
                timeTxt.text = "0" + (int)minute + " : " + "0" + (int)second;
            }
            else
            {
                timeTxt.text = "0" + (int)minute + " : " + (int)second;
            }
        }
        else
        {
            if (second < 10)
            {
                timeTxt.text = (int)minute + " : " + "0" + (int)second;
            }
            else
            {
                timeTxt.text = (int)minute + " : " + (int)second;
            }
        }
    }

    public void CollectCoin()
    {
        audioSource.PlayOneShot(coinSound);
        score += 500;
        scoreTxt.text = "= " + score;
        coinCount++;
        coinTxt.text = "x " + coinCount;
    }

    public void CollectMagnet()
    {
        magnetTime = 10;
        hasMagnet = true;
        audioSource.PlayOneShot(collectiblesSound);
        magnetPnl.SetActive(true);
    }
    public void CollectRocket()
    {
        audioSource.PlayOneShot(collectiblesSound);
        rocketPnl.SetActive(true);
        CoinSpawn();
    }

    public void CollectBoots()
    {
        bootsTime = 10;
        hasBoots = true;
        audioSource.PlayOneShot(collectiblesSound);
        bootsPnl.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pausePnl.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePnl.SetActive(false);
        Time.timeScale = 1;
    }

    public void SettingsBtn()
    {
        settingsPnl.SetActive(true);
    }

    public void Close()
    {
        settingsPnl.SetActive(false);
    }
}
