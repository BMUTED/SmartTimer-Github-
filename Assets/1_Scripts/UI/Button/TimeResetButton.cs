using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeResetButton : MonoBehaviour
{
    int ResetTryNum = 0;

    [SerializeField] TextMeshProUGUI ButtonText;

    private void Start()
    {
        ResetTryNum = 0;
    }

    public void ResetTime()
    {
        ResetTryNum += 1;

        if(ResetTryNum == 1)
        {
            ButtonText.text = "Sure?";
        }

        else if(ResetTryNum == 2)
        {
            GameManager.Instance.SaveManagerSC.SaveData.HourTime = 0;
            GameManager.Instance.SaveManagerSC.SaveData.MinuteTime = 0;
            GameManager.Instance.SaveManagerSC.SaveData.SecondTime = 0;

            ButtonText.text = "Reset";
            ResetTryNum = 0;
        }
    }
}
