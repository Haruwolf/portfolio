
[System.Serializable]
public class MagicMethods {

    //Método construtor, servindo para pegar todas as informações dos scriptable objects

    public Magic_Attributes magic;
    

   public MagicMethods(Magic_Attributes magic)
    {
        this.magic = magic;
        
    }
}

[System.Serializable]
public class EnemyMethods
{

    public Enemy_Attributes enemy;

    public EnemyMethods(Enemy_Attributes enemy)
    {
        this.enemy = enemy;
    }
}

[System.Serializable]
public class StatusMethods
{

    public Status_Attributes status;

    public StatusMethods(Status_Attributes status)
    {
        this.status = status;
    }
}


