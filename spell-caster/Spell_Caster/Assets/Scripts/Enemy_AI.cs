using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

    #region Variaveis de outros scripts
    Enemy_AI_Status statusinfo;
    Enemy_Spawn infoenemy;
    Enemy_Waves infowaves;
    #endregion

    #region FloatPenaltys
    float SpeedPenalty = 1;
    float DamageOverTime;
    #endregion

    #region MagicThings
    [HideInInspector]
    public Status_Dictionary statusdic;
    public Status_Attributes current_status;
    Player_LifeSettings infolifes;
    Game_PointComboControl infopoints;
    #endregion

    #region EnemyThings
    Rigidbody EnemyPhysics;

    GameObject Player;

    [SerializeField]
    float _enemyHP;
    float _destroyAfterPass;

    float multiplicadordeefetividade = 1;
    #endregion

    #region StatusThings
    Status_Attributes Burned;
    Status_Attributes Wet;
    Status_Attributes Marked;
    Status_Attributes Slowed;
    Status_Attributes Paralyzed;
    Status_Attributes None;

    GameObject burntstatus;
    GameObject wetedstatus;
    GameObject markdstatus;
    GameObject slowdstatus;
    GameObject prlststatus;
    GameObject nonetstatus;


    public enum StatusVisual
    {
        Burned,
        Wet,
        Marked,
        Slowed,
        Paralyzed,
        None
    }

    [HideInInspector]
    public StatusVisual statusapp;


    #endregion

    void Start () {

        #region Finds
        //Procura o objeto do Spawn do inimigo e pega os scripts.
        infoenemy = GameObject.Find("EnemySpawnPoint").GetComponent<Enemy_Spawn>();
        infowaves = GameObject.Find("WaveController").GetComponent<Enemy_Waves>();
        infolifes = GameObject.Find("Player").GetComponent<Player_LifeSettings>();
        infopoints = GameObject.Find("MasterControl").GetComponent<Game_PointComboControl>();
        statusdic = GameObject.Find("StatusControl").GetComponent<Status_Dictionary>();
        statusinfo = GameObject.Find("StatusControl").GetComponent<Enemy_AI_Status>();
        //Procura o jogador para comparar mais pra frente a posição
        Player = GameObject.Find("Player");
        #endregion

        #region EnemyAssociations
        _enemyHP = infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyHP; //Coloca no dicionario o nome do inimigo e puxa o HP
        _destroyAfterPass = infoenemy.EnemyLibrary[gameObject.name].enemy.DestroyDistance;
        gameObject.tag = "Enemy";

        EnemyPhysics = gameObject.GetComponent<Rigidbody>();
        EnemyPhysics.mass = infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyWeight;
        #endregion

        #region StatusAssociations
        Burned = statusdic.StatusLibrary["Burn"].status;
        Wet = statusdic.StatusLibrary["Soak"].status;
        Marked = statusdic.StatusLibrary["Mark"].status;
        Slowed = statusdic.StatusLibrary["Slow"].status;
        Paralyzed = statusdic.StatusLibrary["Paralyze"].status;
        None = statusdic.StatusLibrary["Normal"].status;

        burntstatus = statusdic.StatusLibrary["Burn"].status.StatusParticle;
        wetedstatus = statusdic.StatusLibrary["Soak"].status.StatusParticle;
        markdstatus = statusdic.StatusLibrary["Mark"].status.StatusParticle;
        slowdstatus = statusdic.StatusLibrary["Slow"].status.StatusParticle;
        prlststatus = statusdic.StatusLibrary["Paralyze"].status.StatusParticle;
        nonetstatus = statusdic.StatusLibrary["Normal"].status.StatusParticle;

        current_status = statusdic.StatusLibrary["Normal"].status; //O status atual do inimigo vai ser pego da biblioteca e o atributo dela


        //print(infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyType.Immunity);

        //print(gameObject.name);
        //infoenemy.EnemyLibrary[gameObject.name];
        #endregion
    }


    void Update () {



        #region EnemyMovement
        //print(infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyType.MagicType);
        /* O nome do inimigo vai ser o nome de sua chave no dicionário.
         * Ele vai colocar esse nome no game object que irá ser instanciado
         * Nesse script ele vai consultar a biblioteca e vai usar como referencia, o nome do objeto que é o mesmo nome da chave
         * ele vai pegar as propriedades
         * toda chave do dicionário tá associada a um scriptable object */


        gameObject.transform.position -= transform.forward * infoenemy.EnemyLibrary[gameObject.name].enemy.EnemySpeed 
          * SpeedPenalty * Time.deltaTime;
        #endregion

        #region EnemyDeathConditions
        //Compara o Z do inimigo com o Z do personagem, se o Z do inimigo for menor, significa que 
        //O inimigo passou o player, e aí já destrói.
        if (gameObject.transform.position.z < Player.transform.position.z - _destroyAfterPass)
        {
            infowaves.DestroyandCheck(gameObject);
            infolifes.TakeLive();
            infopoints.LostLife();
        }

        if (_enemyHP <= 0)
        {
            infowaves.DestroyandCheck(gameObject);
            
            //Criar um método próprio para destruir e lista
        }
        #endregion


    }

    private void OnCollisionEnter(Collision collision)
    {

        //Como não é possivel atribuir uma palavra a um objeto, foi utilizado tag.
        //Tag tem que estar com o mesmo nome que o "MagicType"

        #region SuperEffective
        //Detectar colisões somente quando tiver uma tag com "Magic"
        if (collision.gameObject.tag == "Magic")
        {
           
            //Atribui a uma nova variavel o valor da variavel do que foi colidido
            //Pega a efetividade da magia que foi lançada. Caso essa efetividade seja igual ao tipo do personagem..
            string Effectiveness = collision.gameObject.GetComponent<Magic_Information>().LaunchedMagicTypeEff;
            if (Effectiveness == infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyType.MagicType)
            {
                //Se possuir o mesmo nome do inimigo, toma o dobro de dano
                multiplicadordeefetividade = 2;
                print("It's Super Effective!");
                infopoints.SuperEffective(); //Já que acertou o inimigo super efetivamente chama o método para dobrar pontos
                Formuladedano(collision, multiplicadordeefetividade);
                StatusChance(collision.gameObject.GetComponent<Magic_Information>().LaunchedStatus);
            }
            #endregion


        #region NotEffective
            //Se por acaso a magia tiver imunidade ao mesmo tipo do inimigo, não causa dano.
            //Ex: Fire, tem Water, se Water for igual ao tipo do inimigo (Water), Fire não dará dano.
            string Immunity = collision.gameObject.GetComponent<Magic_Information>().LaunchedMagicTypeImm;
            if (Immunity == infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyType.MagicType)
            {
                multiplicadordeefetividade = 0;
                infopoints.NotEffective(); //Para zerar o contador
                print("It's not very effective...");
                Formuladedano(collision, multiplicadordeefetividade);
                
                
            }
            #endregion


        #region NeutralEffective
            //Caso a efetividade não exista, e não é imunidade, ele considera que é qualquer magia que acertou.
            if (Effectiveness != infoenemy.EnemyLibrary[gameObject.name].enemy.EnemyType.MagicType)
            {
                infopoints.NeutralEffective(); //5 pontos e zerar contador.
                multiplicadordeefetividade = 0.1f;
                Formuladedano(collision, multiplicadordeefetividade);
            }
            #endregion


        }



    }

    void Formuladedano(Collision magic, float mult)
    {
        
        //Quando o objeto for colidido, ele vai pegar a informação do dano base que tem aquele objeto
        float BaseDmg = magic.gameObject.GetComponent<Magic_Information>().LaunchedBaseDamage;

        Magic_Information call = magic.gameObject.GetComponent<Magic_Information>(); //pegar info do outro script
        call.DestroyMagicApplyEffect(); //chamar o outro método

        //Dependendo de onde ele foi chamado, ele vai multiplicar.
        _enemyHP -= BaseDmg * mult;
        //print(_enemyHP);
    }

    void StatusChance(Status_Attributes status)
    {
        int percentage = Random.Range(0, 10);
        if (percentage <=9)
        {
            current_status = status;
            StatusCheck(current_status.StatusEffect);
        }
    }

    void StatusCheck(string Effect)
    {
        //Funciona tipo um switch, vai checar sempre quando o status ocorrer ou quando acabar.
        if(Effect == Burned.StatusEffect)
        {
            SpeedPenalty = Burned.Speed_Penalty_Multiplier;
            DamageOverTime = Burned.Damage_OverTime;
            StartCoroutine(CreateStatus(DamageOverTime));
            statusapp = StatusVisual.Burned;
            ChangeAppearance(gameObject);
        }

        if (Effect == Wet.StatusEffect)
        {
            SpeedPenalty = Wet.Speed_Penalty_Multiplier;
            DamageOverTime = Wet.Damage_OverTime;
            StartCoroutine(CreateStatus(DamageOverTime));
            statusapp = StatusVisual.Wet;
            ChangeAppearance(gameObject);
        }

        if (Effect == Paralyzed.StatusEffect)
        {
            SpeedPenalty = Paralyzed.Speed_Penalty_Multiplier;
            DamageOverTime = Paralyzed.Damage_OverTime;
            StartCoroutine(CreateStatus(DamageOverTime));
            statusapp = StatusVisual.Paralyzed;
            ChangeAppearance(gameObject);
        }

        if (Effect == Slowed.StatusEffect)
        {
            SpeedPenalty = Slowed.Speed_Penalty_Multiplier;
            DamageOverTime = Slowed.Damage_OverTime;
            StartCoroutine(CreateStatus(DamageOverTime));
            statusapp = StatusVisual.Slowed;
            ChangeAppearance(gameObject);
        }

        if (Effect == Marked.StatusEffect)
        {
            SpeedPenalty = Marked.Speed_Penalty_Multiplier;
            DamageOverTime = Marked.Damage_OverTime;
            StartCoroutine(CreateStatus(DamageOverTime));
            statusapp = StatusVisual.Marked;
            ChangeAppearance(gameObject);
        }

        if (Effect == None.StatusEffect)
        {
            SpeedPenalty = None.Speed_Penalty_Multiplier;
            DamageOverTime = None.Damage_OverTime;
            statusapp = StatusVisual.None;
            ChangeAppearance(gameObject);

        }
    }

    IEnumerator CreateStatus(float dot)
    {
        for (int i = 2; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            _enemyHP -= dot;

        }

        //Joga o status para normal e manda checar os efeitos.
        current_status = statusdic.StatusLibrary["Normal"].status;
        StatusCheck(current_status.StatusEffect);


    }

    public void ChangeAppearance(GameObject enemy)
    {

        switch (statusapp)
        {
            case StatusVisual.Burned:
                burntstatus.gameObject.transform.position = enemy.gameObject.transform.position + new Vector3(0, 10, 0);
                burntstatus.SetActive(statusapp == StatusVisual.Burned);
                break;

            case StatusVisual.Wet:
                wetedstatus.gameObject.transform.position = enemy.gameObject.transform.position + new Vector3(0, 10, 0);
                wetedstatus.SetActive(statusapp == StatusVisual.Wet);
                break;

            case StatusVisual.Marked:
                markdstatus.gameObject.transform.position = enemy.gameObject.transform.position + new Vector3(0, 10, 0);
                markdstatus.SetActive(statusapp == StatusVisual.Marked);
                break;

            case StatusVisual.Slowed:
                slowdstatus.gameObject.transform.position = enemy.gameObject.transform.position + new Vector3(0, 10, 0);
                slowdstatus.SetActive(statusapp == StatusVisual.Slowed);
                break;

            case StatusVisual.Paralyzed:
                prlststatus.gameObject.transform.position = enemy.gameObject.transform.position + new Vector3(0, 10, 0);
                prlststatus.SetActive(statusapp == StatusVisual.Paralyzed);
                break;

            case StatusVisual.None:
                break;
        }
    }



}
