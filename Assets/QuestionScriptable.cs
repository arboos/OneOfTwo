using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "SO/Question")]
public class QuestionScriptable : ScriptableObject
{
    public int p1;
    public int p2;

    public string ru;
    public string en;
    public string tr;
}
