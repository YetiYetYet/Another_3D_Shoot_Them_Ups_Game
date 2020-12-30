using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private Enemy _enemy;
    public Slider lifeEnemyBar;
    public Text lifeEnemyText;
    public Text maxLifeEnemyText;
    
    public Slider lifePlayerBar;
    public Text lifePlayerText;
    public Text maxLifePlayerText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _enemy = Enemy.Instance;
        lifeEnemyBar.maxValue = _enemy.hitPointsPerStates;
        lifeEnemyBar.value = _enemy.hitPointsPerStates;
        lifeEnemyText.text = _enemy.hitPointsPerStates.ToString();
        maxLifeEnemyText.text = _enemy.hitPointsPerStates.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeEnemyBar.value = _enemy.actualLife;
        lifeEnemyText.text = _enemy.actualLife.ToString();
    }
}
