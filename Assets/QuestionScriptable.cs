using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "SO/Question")]
public class QuestionScriptable : ScriptableObject
{
    public int p1;
    public int p2;
    
    public string ru_a;
    public string en_a;
    public string tr_a;
    
    public string ru_b;
    public string en_b;
    public string tr_b;
}
