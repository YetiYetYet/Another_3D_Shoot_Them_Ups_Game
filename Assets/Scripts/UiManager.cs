using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Singleton
    
    private static UiManager _instance;
    public static UiManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    private Enemy _enemy;
    public Slider lifeEnemyBar;
    public Text lifeEnemyText;
    public Text maxLifeEnemyText;

    private Player _player;
    public Slider lifePlayerBar;
    public Text lifePlayerText;
    public Text maxLifePlayerText;

    public Canvas gameUi;
    private CanvasGroup _gameUiCanvasGroup;
    public float fadeDuration;
    public Canvas victory;
    public Canvas gameOver;
    public Canvas pause;

    public void OnEnable()
    {
        GameManager.SwapStateEvent += OnSwap;
    }

    public void OnDisable()
    {
        GameManager.SwapStateEvent -= OnSwap;
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemy = Enemy.Instance;
        lifeEnemyBar.maxValue = _enemy.hitPointsPerStates;
        lifeEnemyBar.value = _enemy.hitPointsPerStates;
        lifeEnemyText.text = _enemy.hitPointsPerStates.ToString();
        maxLifeEnemyText.text = _enemy.hitPointsPerStates.ToString();
        
        _player = Player.Instance;
        lifePlayerBar.maxValue = _player.maxHealth;
        lifeEnemyBar.value = _player.health;
        lifePlayerText.text = _player.health.ToString();
        maxLifePlayerText.text = _player.maxHealth.ToString();
        
        victory.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        gameUi.gameObject.SetActive(true);
        _gameUiCanvasGroup = gameUi.GetComponent<CanvasGroup>();
        _gameUiCanvasGroup.alpha = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameUi.enabled)
        {
            lifeEnemyBar.value = _enemy.actualLife;
            lifeEnemyText.text = _enemy.actualLife.ToString();

            lifePlayerBar.value = _player.health;
            lifePlayerText.text = _player.health.ToString();
        }
    }

    public void OnVictory()
    {
        victory.gameObject.SetActive(true);
    }

    public void OnGameOver()
    {
        gameOver.gameObject.SetActive(true);
    }

    public void OnPause()
    {
        pause.gameObject.SetActive(true);
    }

    public void OnResume()
    {
        victory.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
    }

    public void OnSwap(GameManager gameManager)
    {
        if (gameManager.currentState == GameManager.GameState.Playing)
        {
            _gameUiCanvasGroup.DOFade(1, fadeDuration);
        }
        else
        {
            _gameUiCanvasGroup.DOFade(0, 1);
        }
            
    }

    public void DisableGameUI()
    {
        gameUi.gameObject.SetActive(false);
    }
}
