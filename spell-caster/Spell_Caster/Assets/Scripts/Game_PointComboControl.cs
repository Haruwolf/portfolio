using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_PointComboControl : Game_MasterSettings {


    //Caso o personagem tenha atingido super efetivamente
    public void SuperEffective()
    {
        ComboCounter += 1; //Aumenta o combo counter em um
        GameScore += 10 * ComboCounter; //Ganha 10 pontos + adiciona na contagem do combo
       
        
    }

    public void NeutralEffective()
    {

        ComboCounter = 0; //Zera o contador de combos
        GameScore += 5; //Dá 5 pontos
        
    }

    public void NotEffective()
    {
        ComboCounter = 0; //Zera o contador de pontos
        GameScore += 0; //Não ganha pontos
    }

    public void LostLife()
    {
        ComboCounter = 0; //Zera o contador de combos
        GameScore -= 5; //Perde 5 pontos
    }

    

}
