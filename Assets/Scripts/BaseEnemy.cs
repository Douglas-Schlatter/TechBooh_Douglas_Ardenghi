using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    O enunciado explicitamente pede para que exista um script somente para a movimenta��o
    do inimigo. Eu normalmente faria uma classe "BaseEnemy" como essa que lidaria com os stats, moviment��o e colis�es generalistas de inimigos.
    Posteriormente outros inimigos poderiam herdar dessa classe e se especializar, sendo mais velozes, mais fortes, etc. Fazendo com que existam diversos tipos de inimigos
 */
public class BaseEnemy : MonoBehaviour
{
    //Relacionado a Atributos
    [SerializeField] private int onHitDamage = 1;

 
    void Start()
    {
        
    }


    void Update()
    {
        
    }


    
    /// <summary>
    /// Esse m�todo lidar� com as colis�es do inimigo, para que caso ele colida com o player, ele d� dano no player
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        //Caso o inimigo coldia com o player tire a quantidade de vida que esse inimigo da de dano;
        if (col.tag == "Player")
        {
            PlayerController player = col.GetComponent<PlayerController>();
            player.TakeDamage(onHitDamage);
            this.gameObject.SetActive(false);
        }

    }
}
