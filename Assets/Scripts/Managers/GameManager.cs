using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Chapter Progress
    private bool isPrologueCompleted;
    private bool isChapterOneCompleted;
    private bool isChapterTwoCompleted;
    private bool isChapterThreeCompleted;
    private bool isChapterFourCompleted;
    private bool isChapterFiveCompleted;
    private bool isEpilogueCompleted;
    private bool isGameCompleted;
    #endregion

    #region Chapter Progress
    public bool IsPrologueCompleted
    {
        get { return isPrologueCompleted; }
        set { isPrologueCompleted = value; }
    }

    public bool IsChapterOneCompleted
    {
        get { return isChapterOneCompleted; }
        set { isChapterOneCompleted = value; }
    }

    public bool IsChapterTwoCompleted
    {
        get { return isChapterTwoCompleted; }
        set { isChapterTwoCompleted = value; }
    }

    public bool IsChapterThreeCompleted
    {
        get { return isChapterThreeCompleted; }
        set { isChapterThreeCompleted = value; }
    }

    public bool IsChapterFourCompleted
    {
        get { return isChapterFourCompleted; }
        set { isChapterFourCompleted = value; }
    }

    public bool IsChapterFiveCompleted
    {
        get { return isChapterFiveCompleted; }
        set { isChapterFiveCompleted = value; }
    }

    public bool IsEpilogueCompleted
    {
        get { return isEpilogueCompleted; }
        set { isEpilogueCompleted = value; }
    }

    public bool IsGameCompleted
    {
        get { return isGameCompleted; }
        set
        {
            isGameCompleted = value;
        }
    }
    #endregion
}
