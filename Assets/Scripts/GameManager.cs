﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
    
    public enum GameState
    {
        Pause,
        Playing,
        GameOver,
        Victory,
        Cinematic
    }

    [ReadOnly] public GameState currentState;
    public Camera cinematicCamera;
    public static event Action<GameManager> SwapStateEvent;

    public void OnEnable()
    {
        Player.PlayerDeath += OnPlayerDeath;
        Enemy.NextState += OnNextState;
        Enemy.Die += OnEnemyDie;
    }

    public void OnDisable()
    {
        Player.PlayerDeath -= OnPlayerDeath;
        Enemy.NextState -= OnNextState;
        Enemy.Die -= OnEnemyDie;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        cinematicCamera.gameObject.SetActive(true);
        currentState = GameState.Cinematic;
    }

    public void OnPlayerDeath(Player player)
    {
        SwapState(GameState.GameOver);
    }
    public void ResumeGame()
    {
        SwapState(GameState.Playing);
        cinematicCamera.gameObject.SetActive(false);
    }
    public void SwapState(GameState gameState)
    {
        currentState = gameState;
        SwapStateEvent?.Invoke(this);
        if (currentState == GameState.Cinematic)
        {
            DialogueManager.Instance.StartDialogue();
        }
    }
    
    private void OnEnemyDie(Enemy enemy)
    {
        SwapState(GameState.Victory);
    }
    
    private void OnNextState(Enemy enemy)
    {
        SwapState(GameState.Cinematic);
        DialogueManager.Instance.StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}