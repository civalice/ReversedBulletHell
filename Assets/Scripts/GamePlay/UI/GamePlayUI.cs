using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Urxxx.GamePlay
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI startText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetHp(Player player)
        {
            if (hpText == null) return;
            hpText.text = $"{player.CurrentHp}/{player.MaxHp}";
        }

        public void SetStartText(string text)
        {
            startText.gameObject.SetActive(true);
            startText.text = text;
        }

        public void HideStartText()
        {
            startText.gameObject.SetActive(false);
        }
    }
}