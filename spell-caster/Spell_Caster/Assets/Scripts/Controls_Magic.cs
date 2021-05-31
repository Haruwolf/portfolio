using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Magic : MonoBehaviour {

    //pega informações da classe que possui os estados da magia
    public Controls_Character infos;
    public Magic_Properties _magicp;


    //Estado que o personagem está no momento
    public enum MouseState
    {
        Basic_LeftClick,
        Combine_RightClick
    };

    [Range(0, 5)]
    public float Spell_Position;

    //Começa pelo LeftClick
    MouseState _actualstate = MouseState.Basic_LeftClick;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //Alteração de estados
        if (Input.GetMouseButtonDown(0))
            _actualstate = MouseState.Basic_LeftClick;
        if (Input.GetMouseButtonDown(1))
            _actualstate = MouseState.Combine_RightClick;


        //Pega em qual estado está a magia
		switch(infos.MagiaAtual)
        {
            //Pega as informações do construtor que está pegando dos atributos de magia
            //Associa o objeto que está na lista e a velocidade e passa como parametro para o método par lançar.
            //Se está no estado da magia, a particula da magia é associada a um objeto
            //Chama o método para lançar
            //Se clicar com o botão esquerdo, lança a magia
            case Controls_Character.Magias.Water:
                {
                    float Dmg = infos.MagicLibrary["Water"].magic.BaseDamage;
                    string _effectiveness = infos.MagicLibrary["Water"].magic.Effectiveness;
                    string _immunity = infos.MagicLibrary["Water"].magic.Immunity;
                    GameObject _clone = infos.MagicLibrary["Water"].magic.MagicParticle;
                    float speed = infos.MagicLibrary["Water"].magic.MagicSpeed;
                    float _lifespan = infos.MagicLibrary["Water"].magic.Lifespan;
                    Status_Attributes _mgstatus = infos.MagicLibrary["Water"].magic.MagicStatus;
                    LaunchBasicMagic(_clone,speed,_effectiveness, _immunity, Dmg, _lifespan, _mgstatus);
                    break;
                }

            case Controls_Character.Magias.Fire:
                {
                    float Dmg = infos.MagicLibrary["Fire"].magic.BaseDamage;
                    string _effectiveness = infos.MagicLibrary["Fire"].magic.Effectiveness;
                    string _immunity = infos.MagicLibrary["Fire"].magic.Immunity;
                    GameObject _clone = infos.MagicLibrary["Fire"].magic.MagicParticle;
                    float speed = infos.MagicLibrary["Fire"].magic.MagicSpeed;
                    float _lifespan = infos.MagicLibrary["Fire"].magic.Lifespan;
                    Status_Attributes _mgstatus = infos.MagicLibrary["Fire"].magic.MagicStatus;
                    LaunchBasicMagic(_clone, speed, _effectiveness, _immunity, Dmg, _lifespan, _mgstatus);
                    break;
                }
            case Controls_Character.Magias.Air:
                {
                    float Dmg = infos.MagicLibrary["Air"].magic.BaseDamage;
                    string _effectiveness = infos.MagicLibrary["Air"].magic.Effectiveness;
                    string _immunity = infos.MagicLibrary["Air"].magic.Immunity;
                    GameObject _clone = infos.MagicLibrary["Air"].magic.MagicParticle;
                    float speed = infos.MagicLibrary["Air"].magic.MagicSpeed;
                    float _lifespan = infos.MagicLibrary["Air"].magic.Lifespan;
                    Status_Attributes _mgstatus = infos.MagicLibrary["Air"].magic.MagicStatus;
                    LaunchBasicMagic(_clone, speed, _effectiveness, _immunity, Dmg, _lifespan, _mgstatus);
                    break;
                }
            case Controls_Character.Magias.Thunder:
                {
                    float Dmg = infos.MagicLibrary["Thunder"].magic.BaseDamage;
                    string _effectiveness = infos.MagicLibrary["Thunder"].magic.Effectiveness;
                    string _immunity = infos.MagicLibrary["Thunder"].magic.Immunity;
                    GameObject _clone = infos.MagicLibrary["Thunder"].magic.MagicParticle;
                    float speed = infos.MagicLibrary["Thunder"].magic.MagicSpeed;
                    float _lifespan = infos.MagicLibrary["Thunder"].magic.Lifespan;
                    Status_Attributes _mgstatus = infos.MagicLibrary["Thunder"].magic.MagicStatus;
                    LaunchBasicMagic(_clone, speed, _effectiveness, _immunity, Dmg, _lifespan, _mgstatus);
                    break;
                }
            case Controls_Character.Magias.Earth:
                {
                    float Dmg = infos.MagicLibrary["Earth"].magic.BaseDamage;
                    string _effectiveness = infos.MagicLibrary["Earth"].magic.Effectiveness;
                    string _immunity = infos.MagicLibrary["Earth"].magic.Immunity;
                    GameObject _clone = infos.MagicLibrary["Earth"].magic.MagicParticle;
                    float speed = infos.MagicLibrary["Earth"].magic.MagicSpeed;
                    float _lifespan = infos.MagicLibrary["Earth"].magic.Lifespan;
                    Status_Attributes _mgstatus = infos.MagicLibrary["Earth"].magic.MagicStatus;
                    LaunchBasicMagic(_clone, speed, _effectiveness, _immunity, Dmg, _lifespan, _mgstatus);
                    break;
                }


        }
	}

    void LaunchBasicMagic(GameObject Magic, float Speed, string Effectiveness, string Immunity, float bsdmg, float lifespan, Status_Attributes status)
    {
        //Método de lançamento, se apertar o botão esquerdo, depois que foi chamado, pega os parametros que foi puxado.
        if(Input.GetMouseButtonDown(0))
        {
            //Se apertar o botão, instancia a mágica.
            //Colocar pra magia ser atirada na frente.

           
            //Para que a magia não saia de dentro do cubo.
            Vector3 _spellpos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
                gameObject.transform.position.z + Spell_Position);

           //Armazenar o clone em um objeto e instanciar ele
            GameObject MagicCreation = Instantiate(Magic, _spellpos, Quaternion.identity);
            MagicCreation.AddComponent<Rigidbody>(); //Adicionar Rigidbody nesse clone

            //Adiciona um script na instancia que tem duas variaveis
            MagicCreation.AddComponent<Magic_Information>();

            //Variavel para armazenar as informações do script
            Magic_Information magic_Information = MagicCreation.GetComponent<Magic_Information>();

            //Passa as informações que foram atribuidas antes, nas variaveis desde script
            magic_Information.LaunchedMagicTypeEff = Effectiveness;
            magic_Information.LaunchedBaseDamage = bsdmg;
            magic_Information.LaunchedMagicTypeImm = Immunity;
            magic_Information.LaunchedLifespan = lifespan;
            magic_Information.LaunchedStatus = status;
          

            print(Immunity);

            MagicCreation.tag = "Magic";

            Rigidbody MagicRigidbody = MagicCreation.GetComponent<Rigidbody>(); //Adicionar rigidbody no clone
            MagicRigidbody.useGravity = false; //O clone não usar gravidade
            MagicRigidbody.freezeRotation = true; //O clone não poder girar
            MagicRigidbody.AddForce(transform.forward * Speed * 500 * Time.fixedDeltaTime, ForceMode.Impulse);
            //Lançar o clone, o 500 é o multiplicador para que não seja necessario alterar a speed para valores altos.
           





        }
    }
}
