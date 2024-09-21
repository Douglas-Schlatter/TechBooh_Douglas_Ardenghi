using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
//using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    //Relacionado ao Score
    [SerializeField] private int score = 0;

    //Relacionado ao sistema de spawn do jogo
    [SerializeField] private List<GameObject> spawnPos = new List<GameObject>();
    [SerializeField] private int numbOfEnemys = 0;
    private int lastTargetPos;

    //Relacionado a timer
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float lastSpwan = 0.0f;
    [SerializeField] private float lastHitPlayer;

    //Relacionado a PlayerData
    [SerializeField] private PlayerData playerData;// -> utilizado para manter os scores entre as scenes

    //Relacionado a UI
    [SerializeField] private TextMeshProUGUI scoreTxtValue;
    [SerializeField] private TextMeshProUGUI levelTxtValue;
    [SerializeField] private TextMeshProUGUI totalScoreTxtValue;

    //Relacionado a Eventos -> Para reduzir dependendicias entre classes
    public UnityEvent callUI;

    void Start()
    {
        //No inicio da scene devemos verificar o estado da variavel correctReset do playerData:
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
        //Colocamos a flag do correctReset como false para caso o jogo seja resetado indevidamente podermos captar essa informação depois
        playerData.correctReset = false;
        levelTxtValue.text = playerData.level.ToString();
        scoreTxtValue.text = score.ToString();
        // Colocamos o UiController como observador/listener desse evento, assim podemos chama-lo quando o jogador morrer
        callUI.AddListener(GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>().GameEnded);
    }


    void Update()
    {
        //Implimentei este timer pois multiplos inimigos estavam spawnando no mesmo lugar  no inicio da scene mesmo com a restrição de que o espaço de spawn tenha
        //que ser diferente em spawns seguidos. Acredito que estava acontecendo por que antes de "lastTargetPos" recebecer o valor "targetPos" outro frame ja passava pelo if novamente.
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
        /*
         Aqui estou usando FixedUpdate para a troca de scene ser independente do Frame Rate do jogo
         */

        timer += Time.deltaTime;
        /*Caso o score nesse momento seja maior ou igual ao nivel que estamos * 10, exemplo:
         * Level 1* 10= 10 para prox nivel
         * Level 2* 10= 20 para prox nivel
         * Level 3* 10= 30 para prox nivel
         * Level 4* 10= 40 para prox nivel
         * ...
         * */
        if (score>= playerData.level*10)
        {
            //Atualize os dados em playerData e informe que ele foi resetado de maneira correta
            playerData.score = score;
            playerData.level = playerData.level +1;
            playerData.correctReset = true;
            SceneManager.LoadScene("BattleScene");
        }

    }


    //Implementação das funções que reagem aos eventos ao longo do jogo 

    /// <summary>
    /// Essa função sera utilizada para que o GameManager possa reagir ao evento e um inimigo
    /// morrer, fazendo com que o score aumente e a contagem de imnimigos diminua sem nenhuma conexão
    /// direta com outros scripts
    /// </summary>
    public void EnemyDied() 
    {
        score++;
        scoreTxtValue.text = score.ToString(); 
        numbOfEnemys--;
    
    }


    /// <summary>
    /// Essa função sera utilizada para que o GameManager possa reagir ao evento de quando o player morrer,
    /// fazendo com que a UI seja chamada para mudar os canvas e enviando o Score Final do jogador
    /// sem nenhuma conexão direta com outros scripts
    /// </summary>
    public void CallUI()
    {
        callUI.Invoke();
        totalScoreTxtValue.text = score.ToString();

    }

    /// <summary>
    /// Essa função sera utilizada para que o GameManager possa reagir ao evento de resetar o jogo,
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
