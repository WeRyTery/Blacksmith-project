using System;

[Serializable]
public class CountableDataPreset
{
    public int Reputation;
    public int Money;

    public CountableDataPreset(int reputation, int money)
    {
        Reputation = reputation;
        Money = money;
    }

}
