using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Dictionary : MonoBehaviour {

    public StatusDictionary StatusLibrary;
    public IDictionary<string, StatusMethods> StatusDictionaryControl
    {
        get { return StatusLibrary; }
        set { StatusLibrary.CopyFrom(value); }
    }
    // Use this for initialization
   
}
