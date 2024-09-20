using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //Relacionado a atributos
    [SerializeField] int initialHealth = 2;
    [SerializeField] int health = 2;

    //Relacionado a eventos
    public UnityEvent onDeath;//-> este é o evento que será chamado quando qualquer inimigo morrer, atulizando qualquer coisa relacionada
    void Start()
    {
        // Colocamos o gamecontroler como observador/listener desse evento, assim podemos atualizar a quantidade de inimigos e o score
        onDeath.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().enemyDied); 
    }

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
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
