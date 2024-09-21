using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Possibilita a cria��o de PlayerData no editor da unity
[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")] 
public class PlayerData : ScriptableObject
{
    //Relacionado ao nivel do jogo e score, eles ser�o usados como variaveis de controle no GameManager
    public int score = 0;
    public int level = 0;

    //Utilizado para verificar se a scene foi resetada corretamente ou por meio do programador saindo do PlayMode da unity:
    // correctReset recebe true quando: 10 pontos foram obtidos ou com o jogador morrendo 
    // correctReset recebe false quando: saimos do PlayMode e o Scriptable Object tem de ser resetado
    public bool correctReset = false;
}
