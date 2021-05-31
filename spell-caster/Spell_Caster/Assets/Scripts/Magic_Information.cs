using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Information : MonoBehaviour {

    //Script para ser atrelado ao modelo com informações do scriptable
    public string LaunchedMagicTypeEff; 
    public string LaunchedMagicTypeImm;
    public float LaunchedBaseDamage;
    public float LaunchedLifespan;
    public Status_Attributes LaunchedStatus;

    private void Update()
    {
        //Diminuir o lifespan, se ficar abaixo do 0 é destruído

        LaunchedLifespan -= 1 * Time.deltaTime;
        LaunchedLifespan = Mathf.Clamp(LaunchedLifespan, 0, 3); //Para usar o clamp é preciso atribuir a alguma variavel.
        if (LaunchedLifespan <= 0)
            Destroy(gameObject);
    }

    
    //Quando o objeto encostar no outro inimigo, vai chamar esse método, vai ser usado futuramente para aplicar o efeito.
    public void DestroyMagicApplyEffect()
    {
        Destroy(gameObject);
    }
       
    



}
