using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Magic", menuName = "Create Magic")]
public class Magic_Attributes : ScriptableObject {

    //Atributos da magia
    public int MagicIndex;
    public string MagicName;
    public string MagicType;
    public Sprite MagicSprite;
    public GameObject MagicParticle;
    public string Effectiveness;
    public string Immunity;
    public float BaseDamage;
    public Status_Attributes MagicStatus;
    public float MagicSpeed;
    public string MagicCharacter;
    public float Lifespan;


	
}
