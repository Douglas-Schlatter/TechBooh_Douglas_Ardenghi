using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Relacionado a tracking
    [SerializeField] GameObject player;

    // Relacionado a atributos
    [SerializeField] float speed = 0.1f;
  
    void Start()
    {
        //Encontramos no jogo o GameObject com tag de player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        /*
         * Aqui estou usando diretamente o transform para mover o inimigo
         * entretanto tambem é possivel fazer uma implementação com rigid body, dessa forma poderiamos permitir que o 
         * inimigo interagisse com a fisica do jogo, por exemplo ele poderia ser empurrado.
         * por enquanto deixarei essa implmentação que é mais simples
        */
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }
}
