using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     O enunciado explicitamente pede para que exista um script somente para a movimentação
    do inimigo. Eu normalmente faria uma classe "BaseEnemy" como essa que lidaria com os stats, movimentção e colisões generalistas de inimigos.
    Posteriormente outros inimigos poderiam herdar dessa classe e se especializar, sendo mais velozes, mais fortes, etc. Fazendo com que existam diversos tipos de inimigos
 */
public class BaseEnemy : MonoBehaviour
{
    //Relacionado a Atributos
    [SerializeField] private int onHitDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Inimigo colidiu com: " + col.name);
        //Caso o inimigo coldia com o player tire a quantidade de vida que esse inimigo da de dano;
        if (col.tag == "Player")
        {
            PlayerController player = col.GetComponent<PlayerController>();
            player.TakeDamage(onHitDamage);
            //Destroy(this.gameObject); //-> versão sem ObjectPollig
            this.gameObject.SetActive(false);
        }

    }
}
