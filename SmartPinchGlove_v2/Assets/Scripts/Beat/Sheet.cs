using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    // [SheetInfo]
    public string AudioFileName { set; get; }
    public string AudioViewTime { set; get; }
    public string ImageFileName { set; get; }
    public float Bpm { set; get; }
    public float Offset { set; get; }
    public int Beat { set; get; }
    public int Bit { set; get; }
    public int BarCnt { set; get; }

    // [ContentInfo]
    public string Title { set; get; }
    public string Artist { set; get; }
    public string Source { set; get; }
    public string SheetBy { set; get; }
    public string Difficult { set; get; }

    // [NoteInfo]
    public List<float> noteList1 = new List<float>();
    public List<float> noteList2 = new List<float>();
    public List<float> noteList3 = new List<float>();
    public List<float> noteList4 = new List<float>();


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetNote(int laneNumber, float noteTime)
    {
        if (laneNumber.Equals(1))
        {
            noteList1.Add(noteTime);
        }

        else if (laneNumber.Equals(2))
        {
            noteList2.Add(noteTime);
        }

        else if (laneNumber.Equals(3))
        {
            noteList3.Add(noteTime);
        }

        else if (laneNumber.Equals(4))
        {
            noteList4.Add(noteTime);
        }      
    }

    public void GetNumberOfNotes()
    {
        Data.instance.numberOfNotes = 0;

        Data.instance.numberOfNotes += noteList1.Count();
        Data.instance.numberOfNotes += noteList2.Count();
        Data.instance.numberOfNotes += noteList3.Count();
        Data.instance.numberOfNotes += noteList4.Count();

        Debug.Log(Data.instance.numberOfNotes);
    }

    void showInfo()
    {
        Debug.Log(AudioFileName);
        Debug.Log(Title);
        Debug.Log(Artist);
        Debug.Log(Difficult);
        Debug.Log(Bpm);
    }
}
