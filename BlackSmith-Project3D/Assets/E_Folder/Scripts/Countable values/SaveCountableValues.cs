using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class SaveCountableValues : MonoBehaviour
{
    [SerializeField] public string ValuesDataPath;

    public static List<CountableDataPreset> CountableData = new List<CountableDataPreset>();

    private void Start()
    {
        E_EventBus.SaveCountableValues += SaveValues;
        LoadValues();
    }

    public void SaveValues()
    {
        CountableData.Clear();
        CountableData.Add(new CountableDataPreset(Reputation.GetCurrentReputation(), Money.GetCurrentMoney()));

        Debug.Log("saving countables");
        FileHandler.SaveToJSON<CountableDataPreset>(CountableData, ValuesDataPath);
    }

    public void LoadValues()
    {
        if (CountableData == null || CountableData.Count < 0)
        {
            Debug.Log("No values to load.");
            return;
        }

        CountableData = FileHandler.ReadListFromJSON<CountableDataPreset>(ValuesDataPath);

        Money.LoadMoney(CountableData);
        Reputation.LoadReputation(CountableData);

    }
}
