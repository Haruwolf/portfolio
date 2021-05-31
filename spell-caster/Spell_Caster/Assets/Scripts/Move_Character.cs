using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Character : MonoBehaviour {


    #region Atributos de Posicionamento
    GameObject _main_Character; //Personagem principal
    Vector3 _characterpos; //Posição do personagem
    float _mousepos; //Posição do mouse
    #endregion


    #region Atributos Modificáveis
    [SerializeField]
    [Range(10, 20)]
    float Sensibilidade; //Sensibilidade do mouse

    
    [SerializeField]
    [Range(0, 1)]
    float Margin; //Margem de colisão com o terreno.
    #endregion


    #region Atributos do Terreno
    Collider TerrainObject; //Terreno
    float TerrainBoundsRight; //Limite do terreno direita
    float TerrainBoundsLeft; //Limite do terreno esquerda
    #endregion
    // Use this for initialization
    void Start () {

        #region Encontrar Elementos Principais
        _main_Character = GameObject.Find("Player"); //Procurar objeto Player
        TerrainObject = GameObject.Find("Terrain").GetComponent<Collider>(); //Procurar objeto Terreno
        #endregion

        #region Pegar limites do terreno
        TerrainBoundsRight = TerrainObject.bounds.max.x; //Pega o limite do terreno á direita
        TerrainBoundsLeft = TerrainObject.bounds.min.x; //Pega o limite do terreno á esquerda
        //print(TerrainBoundsLeft);
        //print(TerrainBoundsRight);
        #endregion





    }

    // Update is called once per frame
    void Update () {


        #region Associações
        _characterpos = _main_Character.gameObject.transform.position; //váriavel vai ser igual a posição do objeto personagem
        _mousepos = Input.GetAxis("Mouse X"); //posição do mouse vai ser igual ao movimento X do mouse
        //print(_mousepos);
        #endregion


        #region Responsável pela movimentação

        //váriavel vai ser o acréscimo do posicionamento do mouse vezes a sensibilidade
        _characterpos.x += _mousepos * Sensibilidade * Time.fixedDeltaTime;

        //posição do personagem em si vai ser a posição do personagem, dentro dos limites do terreno esquerda e direita, com a soma da margem)
        _main_Character.gameObject.transform.position = new Vector3(Mathf.Clamp(_characterpos.x, TerrainBoundsLeft + Margin, TerrainBoundsRight-Margin), transform.position.y, transform.position.z);
        #endregion


    }
}
