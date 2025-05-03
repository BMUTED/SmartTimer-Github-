using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public SaveDatas SaveData = new SaveDatas();

    public PriorityEvent OnSaveFileLoaded = new PriorityEvent();
     
    private void Awake()
    {
        SaveData = LoadingData(SaveData.FileName);
        OnSaveFileLoaded.Invoke();
    }

    private void Start()
    {
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(() => SavingData(SaveData.FileName, SaveData), 20);
    }

    /// <summary>
    /// 애플리케이션이 종료될때 호출되는 함수
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("애플리케이션이 종료됩니다. 데이터를 저장합니다");
        //프로그램 끌 때, 자동 저장
        SavingData(SaveData.FileName, SaveData);
    }

    /// <summary>
    /// SaveData클래스의 데이터 스크립트를 특정 파일 경로에 저장하는 함수
    /// (이미 동일한 경로에 파일이 있는 경우, 데이터를 덮어씌우고, 없는 경우 새롭게 생성함)
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="SaveFile"></param>
    public static void SavingData(string fileName, SaveDatas SaveFile)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // 파일 스트림을 엽니다. 파일이 없으면 새로 생성합니다.
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            // BinaryFormatter를 사용하여 객체를 직렬화합니다.
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, SaveFile);
        }
    }

    // 파일에서 SaveData를 불러오는 함수
    public static SaveDatas LoadingData(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // 파일이 없으면 null을 반환
        if (!File.Exists(filePath))
        {
            Debug.Log($"{fileName} 이라는 이름의 파일이 존재하지 않아, 불러오지 않았습니다");
            Debug.Log("대신 세이브 데이터를 새롭게 생성하였습니다");
            return new SaveDatas();
        }

        // 파일 스트림을 열고, BinaryFormatter를 사용하여 직렬화된 객체를 복원합니다.
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Debug.Log($"{fileName} 이라는 이름의 파일을 불러오는데 성공하였습니다");
            return (SaveDatas)formatter.Deserialize(fileStream);  // 직렬화된 데이터를 복원
        }
    }
}
