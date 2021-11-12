using UnityEngine;
using UnityEngine.Serialization;

namespace Eskills.Scripts.Ui
{
    [CreateAssetMenu]
    [System.Serializable]
    public class MatchParameters : ScriptableObject
    {
        [SerializeField] public string userName;
        [SerializeField] public float value;
        [SerializeField] public string currency = "USD";
        [SerializeField] public string product;
        [SerializeField] public int timeout = 600;
        [SerializeField] public MatchEnvironment matchEnvironment;
        [SerializeField] public int numberOfPlayers = 2;

        public override string ToString()
        {
            return "UserName: " + userName + " Value: " + value + " Currency: " + currency + " Product: " + product + " Timeout: " +
                   timeout + " MatchEnvironment: " + matchEnvironment + " NumberOfPlayers: " + numberOfPlayers;
        }
    }
}