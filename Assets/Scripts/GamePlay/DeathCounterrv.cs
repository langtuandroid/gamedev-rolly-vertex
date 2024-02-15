using UnityEngine;

public class DeathCounterrv : MonoBehaviour 
{
    public static int counterrv = 0;

    private static DeathCounterrv Instancerv;

    private void Awake()
    {
        if (Instancerv) Destroy(gameObject);
        else Instancerv = this;

        DontDestroyOnLoad(gameObject);
    }
}