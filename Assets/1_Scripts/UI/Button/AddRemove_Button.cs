using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemove_Button : MonoBehaviour
{
    public List<GameObject> RegisterButtons;

    public GameObject ButtonGroup; //소환한 RegisterButton들의 부모가 될 오브젝트
    public GameObject RegisterPrefab; //소환한 RegisterButton의 프리팹

    [SerializeField] int MaxButtonNum = 11;
    [SerializeField] int MinButtonNum = 1;

    private void Start()
    {
        if(RegisterButtons == null)
        {
            Debug.Log("RegisterButtons 라는 리스트가, 인스펙터 창에서 수정되지 않았던지라 새롭게 초기화되었습니다");
            RegisterButtons = new List<GameObject>();
        }

        SpawnRegisterButton();
    }

    /// <summary>
    /// 세이브 파일 속 RegistedProgramNum 에 맞춰, 버튼을 생성하는 함수
    /// </summary>
    void SpawnRegisterButton()
    {
        for (int i = 0; i < GameManager.Instance.SaveManagerSC.SaveData.RegistedProgramNum-1; i++)
        {
            if (RegisterButtons.Count < MaxButtonNum) //버튼이 11개 미만일때 까지만
            {
                //버튼 생성 기능 실행
                GameObject NewRegisterButton = Instantiate(RegisterPrefab, transform.position, Quaternion.identity, ButtonGroup.transform);
                RegisterButtons.Add(NewRegisterButton);

                NewRegisterButton.GetComponent<RegisterButton>().Index = RegisterButtons.Count - 1;
            }
        }
    }

    public void AddRegeisterButton()
    {
        if(RegisterButtons.Count < MaxButtonNum) //버튼이 11개 미만일때 까지만
        {
            //버튼 생성 기능 실행
            GameObject NewRegisterButton = Instantiate(RegisterPrefab, transform.position, Quaternion.identity, ButtonGroup.transform);
            RegisterButtons.Add(NewRegisterButton);

            NewRegisterButton.GetComponent<RegisterButton>().Index = RegisterButtons.Count-1;

            GameManager.Instance.SaveManagerSC.SaveData.RegistedProgramNum += 1;

            GameManager.Instance.ScreenSizeManagerSC.AdjustScreenSizeAsButtonNum();
        }
    }

    public void RemoveRegeisterButton()
    {
        if (RegisterButtons.Count > MinButtonNum) //버튼이 1개 이상일때만
        {
            //버튼 삭제 기능 실행
            RegisterButtons[RegisterButtons.Count - 1].GetComponent<RegisterButton>().RemoveCurrentProgramOnButton(); //매니저 속 정보
            Destroy(RegisterButtons[RegisterButtons.Count - 1]); //가장 마지막으로 생성했던 버튼 삭제
            RegisterButtons.RemoveAt(RegisterButtons.Count - 1); //리스트에서도 삭제

            GameManager.Instance.SaveManagerSC.SaveData.RegistedProgramNum -= 1;

            GameManager.Instance.ScreenSizeManagerSC.AdjustScreenSizeAsButtonNum();
        }
        else
        {
            Debug.Log($"현재 리스트의 Count가 {RegisterButtons.Count} 개라서, 버튼 삭제가 이행되지 않았습니다");
        }    
    }
}
