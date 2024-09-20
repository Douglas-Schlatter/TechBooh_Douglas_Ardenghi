using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Relacionado ao Score
    [SerializeField] int score = 0;

    //Relacionado ao sistema de spawn do jogo
    [SerializeField] List<GameObject> spawnPos = new List<GameObject>();
    [SerializeField] int numbOfEnemys = 0;
    int lastTargetPos;

    //Relacionado a timer
    public float timer = 0.0f;
    public float lastSpwan = 0.0f;
    public float lastHitPlayer;

    //Relacionado a PlayerData
    [SerializeField] PlayerData playerData;// -> utilizado para manter os scores entre as scenes

    void Start()
    {
        //No inicio da scene devemos verificar o estado da correcReset do playerData:
        // correctReset recebe true quando: 10 pontos foram obtidos ou com o jogador morrendo -> logo continuamos o jogo normalmente recebendo o score passado pelo playerData
        // correctReset recebe false quando: saimos do PlayMode e o Scriptable Object tem de ser resetado -> logo devemos resetar o estado de playerData para o inicio do jogo, resetando seu score e nivel
        if (playerData.correctReset)
        {
            score = playerData.score;
        }
        else 
        {
            playerData.score = 0;
            playerData.level = 1;
        }
        //Colocamos a flag do correctReset como false para caso o jogo seja resetado indevidamente podermos manter essa informação
        playerData.correctReset = false;
    }


    void Update()
    {
        //Implimentei este timer pois multiplos inimigos estavam spawnando no mesmo lugar  no inicio da scene mesmo com a restrição de que o espaço de spawn tinha
        //que ser diferente a cada spawn. Acredito que estava acontecendo por que antes de "lastTargetPos" recebece "targetPos" outro frame ja passava pelo if novamente.
        if ((timer - lastSpwan) > 0.15)
        {
            //Quero manter sempre 5 inimigos na tela
            if (numbOfEnemys < 5)
            {
                lastSpwan = timer;
                //Sorteie um spawnpoint que não seja o ultimo selecionado
                int targetPos = Random.Range(1, spawnPos.Count);
                if (targetPos != lastTargetPos)
                {
                    lastTargetPos = targetPos;
                    //Tente pegar um inimigo disponivel para ser reciclado
                    GameObject enemy = ObjectPool.instance.GetPooledEnemy();
                    if (enemy != null)
                    {
                        //se existir coloque-o na posição de spawn e o o ative
                        enemy.transform.position = spawnPos[targetPos].transform.position;
                        enemy.transform.rotation = spawnPos[targetPos].transform.rotation;
                        enemy.SetActive(true);
                        numbOfEnemys++;
                    }
                }
            }
        }

    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        //Score não é zero e é divisivel por 10
        if (score>= playerData.level*10)
        {
            //Debug.Log("Troque de scene");
            //Atualize os dados em playerData e informe que ele foi resetado de maneira correta
            playerData.score = score;
            playerData.level = playerData.level +1;
            playerData.correctReset = true;
            SceneManager.LoadScene("BattleScene");
        }

    }

    /// <summary>
    /// Essa função sera utilizada para que o GameManager possa reagir ao eventod e um inimigo
    /// morrendo, fazendo com que o score aumente o numero de imnimigos diminua sem nenhuma conexão
    /// direta com outros scripts
    /// </summary>
    public void EnemyDied() 
    {
        score++;
        numbOfEnemys--;
    
    }
    /// <summary>
    /// Essa função sera utilizada para que o GameManager possa reagir ao evento de  fim de jogo quando o player morrer,
    /// fazendo com que o jogo resete e armazenando que o jogo foi resetado corretamente no playerData
    /// direta com outros scripts
    /// </summary>
    public void GameEnded()
    {
        playerData.score = 0;
        playerData.level = 1;
        playerData.correctReset = true;
        SceneManager.LoadScene("BattleScene");
    }

}
