using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Waves : MonoBehaviour {

    Enemy_Spawn infoenemy;
    int CurrentWave;


	void Start () {

        infoenemy = GameObject.Find("EnemySpawnPoint").GetComponent<Enemy_Spawn>();
        CurrentWave = 1;
        infoenemy.StartWave = CurrentWave;

    }
	
    //Método para retirar o inimigo da listagem, para destruir o inimigo e ver se chegou na contagem 0
	public void DestroyandCheck(GameObject enemy)
    {

        infoenemy.screenenemylist.Remove(enemy);
        Destroy(enemy);

        if(infoenemy.screenenemylist.Count <= 0)
        {
            //Se chegou na contagem 0 manda o Spawn chamar a próxima wave
            StartCoroutine(NextWave());
        }
        //print(infoenemy.screenenemylist.Count);
    }

    //Espera 3 segundos, aumenta o contador de wave, e chama o método de novo para iniciar uma nova wave
    //Passando como parametro o número da Wave atual.
    IEnumerator NextWave ()
    {
        yield return new WaitForSeconds(3.0f);
        CurrentWave += 1;
        infoenemy.StartSpawn(CurrentWave);
        print(CurrentWave);

    }
}
