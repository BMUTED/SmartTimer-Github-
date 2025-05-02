using System;
using System.Collections.Generic;
[Serializable]  // BinaryFormatter�� [Serializable] Ư���� �־�� �۵��ϱ⵵, �ϰ� �����Ϳ��� Ȯ���� �뵵�� Serializable�� ���
public class SaveDatas 
{
    //Ȯ���ڴ� �ƹ� �ǹ� ����, �ƹ��ų� �ᵵ �� (�Ʒ��� Save �����̶�� ���� ���񿡼� ����ϱ� ���� save�� �ۼ���)
    public string FileName => "TimerData.save";

    /// <summary>
    /// ��ϵ� ���α׷��� ����, �޴� ������ �Ѿ����, ��ư�� ����� �����Ұ��� �����ϴµ� ����
    /// </summary>
    public int RegistedProgramNum = 1;

    /// <summary>
    /// ���̺� ���Ͽ� �����, ��ϵ� ���α׷� ����Ʈ (ProgramCheck�� TargetProcessName ������ �����ϰų�, ����ﶧ ���)
    /// </summary>
    public List<string> RegisterdProcessNames;

    //�ð��� ���õ� �����͵�
    public float HourTime;
    public float MinuteTime;
    public float SecondTime;
}
