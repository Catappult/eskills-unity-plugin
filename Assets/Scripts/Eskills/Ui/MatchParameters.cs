using Eskills.Service;
using UnityEngine;

namespace Eskills.Ui
{
    [CreateAssetMenu]
    [System.Serializable]
    public class MatchParameters : ScriptableObject
    {
        [SerializeField] public float value;
        [SerializeField] public string currency = "USD";
        [SerializeField] public string product;
        [SerializeField] public int timeout = 600;
        [SerializeField] public MatchEnvironment matchEnvironment;
        [SerializeField] public int numberOfPlayers = 1;

        public override string ToString()
        {
            return " Value: " + value + " Currency: " + currency + " Product: " + product +
                   " Timeout: " +
                   timeout + " MatchEnvironment: " + matchEnvironment + " NumberOfPlayers: " + numberOfPlayers;
        }
    }
}