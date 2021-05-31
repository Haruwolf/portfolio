using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LifeSettings : Game_MasterSettings {
    //Herdado diretamente das configs mestres
    public int Player_Lifes = 3;

    //O inimigo passará pelo personagem e chamará esse método
    public void TakeLive()
    {
        //Checará toda vez que o personagem perder uma vida se a vida dele for igual a 0
        Player_Lifes -= 1;
        print(Player_Lifes);
        if (Player_Lifes <= 0)
        {
            //Se for, chama o GameOver
            GameOver();
        }
   
    }

}
