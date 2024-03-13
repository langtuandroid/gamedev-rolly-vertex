using System;
using UnityEngine;

namespace GamePlay
{
    public class DeathCounterrv : MonoBehaviour 
    {
        public static int Counterrv = 0;

        private static DeathCounterrv Instancerv;

        private void Awake()
        {
            if (Instancerv) Destroy(gameObject);
            else Instancerv = this;

            DontDestroyOnLoad(gameObject);
        }
        
        private double Calculaterv(double number)
        {
            return Math.Pow(number, 1.0 / 3.0);
        }
    }
}