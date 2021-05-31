using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game_MasterSettings : MonoBehaviour {


    //Lugar onde as coisas principais do jogo estão.
    public int GameScore = 0;
    public int ComboCounter = 0;
    //Chama o GameOver
    public void GameOver()
    {
        Scene currentscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentscene.name);

    }


    //Provisório
    private void Update()
    {
        print("O Score atual é:" + GameScore);
        
    }
}
