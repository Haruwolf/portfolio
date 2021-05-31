using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Status", menuName = "Create Status")]
public class Status_Attributes : ScriptableObject {

    
    public GameObject StatusParticle;
    public float Speed_Penalty_Multiplier;
    public int Damage_OverTime;
    public string StatusEffect;
    public Magic_Attributes ComboWith;


}
