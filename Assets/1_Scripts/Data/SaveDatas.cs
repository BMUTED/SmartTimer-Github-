using System;
using System.Collections.Generic;
[Serializable]  // BinaryFormatter는 [Serializable] 특성이 있어야 작동하기도, 하고 에디터에서 확인할 용도로 Serializable로 사용
public class SaveDatas 
{
    //확장자는 아무 의미 없음, 아무거나 써도 됨 (아래는 Save 파일이라는 점을 제목에서 명시하기 위해 save로 작성함)
    public string FileName => "TimerData.save";

    /// <summary>
    /// 등록된 프로그램의 갯수, 메뉴 씬으로 넘어갔을때, 버튼을 몇개까지 생성할건지 결정하는데 사용됨
    /// </summary>
    public int RegistedProgramNum = 1;

    /// <summary>
    /// 세이브 파일에 저장될, 등록된 프로그램 리스트 (ProgramCheck의 TargetProcessName 값들을 저장하거나, 덮어씌울때 사용)
    /// </summary>
    public List<string> RegisterdProcessNames;

    //시간과 관련된 데이터들
    public float HourTime;
    public float MinuteTime;
    public float SecondTime;
}
