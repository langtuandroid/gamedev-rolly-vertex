using UnityEngine;

[CreateAssetMenu(menuName = "PurchasePrduct")]
public class PurchasePrduct : ScriptableObject
{
    public TypeBallUnlock _typeBallUnlock;
    public Material _material;
    public int price;
    public bool adsIsWatchComplete;
    public bool UnlockStatus;
}

public enum TypeBallUnlock
{
    ForCoins,
    ForDiamonds,
    ForAd
}
