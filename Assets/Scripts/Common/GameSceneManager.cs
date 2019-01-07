using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    [SerializeField]
    private string m_keyconfigPath;

    private KeyConfig m_keyConfig;
    // Use this for initialization
    void Start () {
        m_keyConfig = new KeyConfig( Application.dataPath + m_keyconfigPath);
        m_keyConfig.LoadConfigFile();
        m_keyConfig.SaveConfigFile();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
