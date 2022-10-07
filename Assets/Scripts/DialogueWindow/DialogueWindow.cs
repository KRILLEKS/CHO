using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private byte dialogueID;
    [SerializeField] private byte charpersec;

    private TextMeshProUGUI _outputtxt;
    private Button _nextbutton;
    private bool _cactive, _gonext;

    private DialogueInformation _di = new DialogueInformation();
    private List<string> _strlist = new List<string>();

    void Start()
    {
        _outputtxt = this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        _nextbutton = this.transform.GetChild(1).GetComponent<Button>();
        _nextbutton.onClick.AddListener(GoAhead);

        _di = JsonUtility.FromJson<DialogueInformation>(File.ReadAllText(Application.dataPath + $"/Json/Dialogue{dialogueID}.json"));
        string[] strings = _di.loadedstring.Split('|');
        foreach (string s in strings)
        {
            _strlist.Add(s.Replace("/", System.Environment.NewLine));
        }

        StartCoroutine(ShowInformation());
    }

    void GoAhead()
    {
        _gonext = true;
    }

    IEnumerator ShowInformation()
    {
        //START OF DIALOGUE

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _strlist.Count; i++)
        {
            _outputtxt.text = "";
            StartCoroutine(PrintText(_strlist[i]));
            yield return new WaitUntil(() => !_cactive);
        }

        //END OF DIALOGUE
    }

    IEnumerator PrintText(string input)
    {
        _cactive = true;
        for (int i = 1; i <= input.Length; i++)
        {
            if (!_gonext)
            {
                _outputtxt.text = input.Substring(0, i);
                yield return new WaitForSeconds(1f / charpersec);
            }
            else
            {
                _outputtxt.text = input;
                _gonext = false;
                break;
            }
        }
        yield return new WaitUntil(() => _gonext);
        _gonext = false;
        _cactive = false;
    }
}

[Serializable]
public class DialogueInformation
{
    public string loadedstring;
}