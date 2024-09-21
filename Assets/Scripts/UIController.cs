using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    // Relacionado a UI
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;




    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    /// <summary>
    /// O jogador morreu! Ative a tela de gameOver e pause o jogo
    /// </summary>
    public void GameEnded()
    {
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// O botão de resetar foi ativado, ative o canvas de jogo e despause o jogo
    /// </summary>
    public void GameReset()
    {
        inGameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
    }


}
