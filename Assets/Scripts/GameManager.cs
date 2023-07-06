using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private int coins;
    private float timer;
    private GameState state;
    

    [SerializeField]
    private GameObject canvas, losePanel;
    private TMP_Text coinText,timeText;

    enum GameState
    {
        Playing,
        Shop,
        Pause
    }
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        EventHandler.Instance.OnZombieDeath.AddListener(AddCoins);

        coins = 0;
        timer = 0f;
        canvas.SetActive(false);
        state=GameState.Playing;

        coinText=GameObject.Find("CoinText").GetComponent<TMP_Text>();
        timeText=GameObject.Find("Timer").GetComponent<TMP_Text>();


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOpening();
        }
        coinText.text = coins.ToString();
        timeText.text = string.Format("{0:mm\\:ss\\:ff}", TimeSpan.FromSeconds(timer));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer+=Time.fixedDeltaTime;
    }
    private void AddCoins()
    {
        coins+= UnityEngine.Random.Range(1,6);
    }
    public void DeductCoins(int num) 
    {
        coins-=num;
    }
    public void MenuOpening()
    {
        if(losePanel.activeInHierarchy)
        {
            return;
        }

        if(state==GameState.Playing)
        {
            state = GameState.Shop;
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }
        else if(state==GameState.Shop)
        {
            state = GameState.Playing;
            Time.timeScale = 1f;
            canvas.SetActive(false);
        }   
    }
    public int getCoins()
    {
        return coins;
    }
    public void OpenLosePanel()
    {
        Time.timeScale= 0f;
        losePanel.SetActive(true);
    }
}
