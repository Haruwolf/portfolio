
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour {


    public List<GameObject> screenenemylist = new List<GameObject>();

    int EnemyQuantity;
    public int StartWave;
    public float SpawnInterval = 2;

    public EnemyDictionary EnemyLibrary; //Cria uma váriavel para puxar e armazenar inimigos
    public IDictionary<string, EnemyMethods> Enemy_Dictionary //Cria um dicionário serializável com String e o método construtor
        //que pega todos os atributos dos inimigos
    {
        get { return EnemyLibrary; }
        set { EnemyLibrary.CopyFrom(value); }
    }

    [SerializeField]
    int MinWaveSpawn;

    [SerializeField]
    int MaxWaveSpawn;


    #region Atributos do Terreno
    Collider TerrainObject; //Terreno
    float TerrainBoundsRight; //Limite do terreno direita
    float TerrainBoundsLeft; //Limite do terreno esquerda
    #endregion



    // Use this for initialization
   void Start () {

        #region InfoTerreno
        #region Achar Objeto Terreno
        TerrainObject = GameObject.Find("Terrain").GetComponent<Collider>(); //Procurar objeto Terreno
        #endregion

        #region Pegar limites do terreno
        TerrainBoundsRight = TerrainObject.bounds.max.x; //Pega o limite do terreno á direita
        TerrainBoundsLeft = TerrainObject.bounds.min.x; //Pega o limite do terreno á esquerda

        #endregion
        #endregion

        StartSpawn(StartWave);

    }

    public void StartSpawn(int CurrentWave)
    {
        //Cria a quantidade de inimigos que a horda vai ter
        EnemyQuantity = Random.Range(MinWaveSpawn, MaxWaveSpawn) + CurrentWave;
        //Passa essa quantidade para a Coroutine
        StartCoroutine(EnemyHordeSpawn(EnemyQuantity));
    }

    IEnumerator EnemyHordeSpawn(int enemyquantity)
    {
        //***Obs, se o terreno ficar muito pequeno,os inimigos passam, se ficar muito grande, eles se concentram numa área
        //*** Talvez dê pra usar a posição do objeto como ponto de referencia.
        //Vai spawnar inimigos na quantidade que foi pre-definida
        for (int eq = enemyquantity; eq >= 0; eq--)
        {
            //espera um tempo para spawnar os inimigos
            yield return new WaitForSeconds(SpawnInterval);
            List<string> _listadeinimigos = new List<string>(Enemy_Dictionary.Keys);
            //Cria um index onde a chave o index é selecionado aleatoriamente
            int randomenemy = Random.Range(0, Enemy_Dictionary.Keys.Count);
            
            //Baseado no tamanho do terreno, a posição do inimigo será feita a partir das bordas com uma diferença aleatoria no terreno
            Vector3 enemypos = new Vector3(TerrainBoundsLeft + Random.Range(0,5) + TerrainBoundsRight - Random.Range(0,5),
               gameObject.transform.position.y, gameObject.transform.position.z);

           
            //Como a lista contém todos as chaves, ele escolhe um index aleatoriamente, com isso o valor vai ser colocado
            //no lugar de _listadeinimigos e pegar o inimigo para ser instanciado.
            GameObject screen_enemy = Instantiate(EnemyLibrary[_listadeinimigos[randomenemy]].enemy.EnemyModel, enemypos, Quaternion.identity);
            screen_enemy.name = _listadeinimigos[randomenemy];
            
            //Adiciona os inimigos em uma lista para se ter controle;
            screenenemylist.Add(screen_enemy);

            /*print("A quantidade de inimigos na tela é" + screenenemylist.Count);
            print("A quantidade de inimigos para lançar ainda é:" + eq);*/
            //print(screenenemylist.Count);


        }

        
        //*************Detectar quando a contagem de inimigos na tela zerou.*****************

        /*foreach (GameObject enemys in screenenemylist)
        {
            print(enemys.name);
        }*/



    }

}
