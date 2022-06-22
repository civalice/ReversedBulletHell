using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Urxxx.GamePlay
{
    public class UpgradeUI : MonoBehaviour
    {
        public Button Button01;
        public Button Button02;
        public Button Button03;

        public Text Button01Txt;
        public Text Button02Txt;
        public Text Button03Txt;

        protected UpgradeInfo Upgrade01;
        protected UpgradeInfo Upgrade02;
        protected UpgradeInfo Upgrade03;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupUpgrade(List<UpgradeInfo> list)
        {
            if (list.Count >= 3)
            {
                Upgrade03 = list[2];
                Button03Txt.text = Upgrade03.GetStringUpgrade();
            }
            else
            {
                Button03.gameObject.SetActive(false);
            }

            if (list.Count >= 2)
            {
                Upgrade02 = list[1];
                Button02Txt.text = Upgrade02.GetStringUpgrade();
            }
            else
            {
                Button02.gameObject.SetActive(false);
            }

            if (list.Count >= 1)
            {
                Upgrade01 = list[0];
                Button01Txt.text = Upgrade01.GetStringUpgrade();
            }
            else
            {
                Button01.gameObject.SetActive(false);
            }
            gameObject.SetActive(true);
        }

        public void UpgradeButtonPressed(int index)
        {
            switch (index)
            {
                case 1:
                    GameController.Instance.ChooseUpgrade(Upgrade01);
                    break;
                case 2:
                    GameController.Instance.ChooseUpgrade(Upgrade02);
                    break;
                case 3:
                    GameController.Instance.ChooseUpgrade(Upgrade03);
                    break;
            }
            //clear choice
            Upgrade01 = null;
            Upgrade02 = null;
            Upgrade03 = null;
            gameObject.SetActive(false);
        }
    }
}