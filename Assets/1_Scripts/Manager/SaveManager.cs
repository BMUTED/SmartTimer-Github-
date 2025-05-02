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
    /// ���ø����̼��� ����ɶ� ȣ��Ǵ� �Լ�
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("���ø����̼��� ����˴ϴ�. �����͸� �����մϴ�");
        //���α׷� �� ��, �ڵ� ����
        SavingData(SaveData.FileName, SaveData);
    }

    /// <summary>
    /// SaveDataŬ������ ������ ��ũ��Ʈ�� Ư�� ���� ��ο� �����ϴ� �Լ�
    /// (�̹� ������ ��ο� ������ �ִ� ���, �����͸� ������, ���� ��� ���Ӱ� ������)
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="SaveFile"></param>
    public static void SavingData(string fileName, SaveDatas SaveFile)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // ���� ��Ʈ���� ���ϴ�. ������ ������ ���� �����մϴ�.
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            // BinaryFormatter�� ����Ͽ� ��ü�� ����ȭ�մϴ�.
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, SaveFile);
        }
    }

    // ���Ͽ��� SaveData�� �ҷ����� �Լ�
    public static SaveDatas LoadingData(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // ������ ������ null�� ��ȯ
        if (!File.Exists(filePath))
        {
            Debug.Log($"{fileName} �̶�� �̸��� ������ �������� �ʾ�, �ҷ����� �ʾҽ��ϴ�");
            return null;
        }

        // ���� ��Ʈ���� ����, BinaryFormatter�� ����Ͽ� ����ȭ�� ��ü�� �����մϴ�.
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Debug.Log($"{fileName} �̶�� �̸��� ������ �ҷ����µ� �����Ͽ����ϴ�");
            return (SaveDatas)formatter.Deserialize(fileStream);  // ����ȭ�� �����͸� ����
        }
    }
}
