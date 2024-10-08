using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //Relacionado a atributos
    [SerializeField] private int initialHealth = 2;
    [SerializeField] private int health = 2;

    //Relacionado a eventos
    public UnityEvent onDeath;//-> este � o evento que ser� chamado quando qualquer inimigo morrer, atulizando qualquer coisa relacionada
    void Start()
    {
        // Colocamos o gamecontroler como observador/listener desse evento, assim podemos atualizar a quantidade de inimigos e o score
        onDeath.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().EnemyDied); 
    }

    /// <summary>
    /// Fun��o responsavel por administrar o dano recebido por personagens
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {


        health -= damage;

        //Se o inimigo morreu, ative o evento onDeath, resete a vida do inimigo e o desative.
        if (health <= 0)
        {
            onDeath.Invoke();
            health = initialHealth;
            this.gameObject.SetActive(false);
        }

    }
    



    void Update()
    {
        
    }
}
