using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Character : MonoBehaviour {

    string _inputkey = ""; //Armazena o que o jogador apertou


    //Cria uma lista com o construtor declarado para associar os scriptable objects, sendo assim, pega todas as informações
    //O construtor serve para pegar e passar valores, principalmente aqueles que precisam ser instanciados depois
    //Não é possivel instanciar uma váriavel usando outra variável de instancia, é preciso usar um construtor para
    //pegar todas as informações daquela variavel instanciada
    //E assim, criar uma lista que irá ter os objetos e seus valores.
    public List<MagicMethods> magicMethods = new List<MagicMethods>();
    public MagicDictionary MagicLibrary;
    public IDictionary<string, MagicMethods> MagicDictionary
    {
        get { return MagicLibrary; }
        set { MagicLibrary.CopyFrom(value); }
    }



    

    #region Armazena as teclas
    //Armazena cada tecla que está associada a uma magia
    string _waterKey;
    string _fireKey;
    string _airKey;
    string _thunderKey;
    string _earthKey;
    #endregion


    public enum Magias
    {
        None,
        Water,
        Fire,
        Air,
        Thunder,
        Earth
    };

    public Magias MagiaAtual = Magias.None;


    //TENTAR USAR O CONSTRUTOR!!
    // Use this for initialization
    void Start () {

        _waterKey = MagicLibrary["Water"].magic.MagicCharacter;
        //Associa a tecla que está com a magia, na variavel
        _fireKey = MagicLibrary["Fire"].magic.MagicCharacter;
        _airKey = MagicLibrary["Air"].magic.MagicCharacter;
        _thunderKey = MagicLibrary["Thunder"].magic.MagicCharacter;
        _earthKey = MagicLibrary["Earth"].magic.MagicCharacter;
    }

	
	// Update is called once per frame
 
	void Update () {

        //Detectar qual tecla que foi apertada
        foreach (char c in Input.inputString)
        {
            _inputkey = c.ToString();

            //Se for a string que está com valor colocado, tem que associar ao script
            //Ao apertar a tecla corretamente, ele entra no estado da magia.
            if (_inputkey.ToUpper() == _waterKey)
            {
                MagiaAtual = Magias.Water;
            }

            if (_inputkey.ToUpper() == _fireKey)
            {
                MagiaAtual = Magias.Fire;
            }
            if (_inputkey.ToUpper() == _airKey)
            {
                MagiaAtual = Magias.Air;
            }
            if (_inputkey.ToUpper() == _thunderKey)
            {
                MagiaAtual = Magias.Thunder;
            }
            if (_inputkey.ToUpper() == _earthKey)
            {
                MagiaAtual = Magias.Earth;
            }



        }
         
		
	}
}
