using UnityEngine;
using UnityEngine.UI;

namespace Eskills.Ui
{
    public class TextUserNameProvider : UserNameProvider
    {
        [SerializeField] private Text _userNameText;

        public override string GetUserName()
        {
            return _userNameText.text;
        }
    }
}