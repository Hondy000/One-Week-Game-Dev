using Assets.Scripts.Common.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


[Serializable]
public class KeyData
{

    [SerializeField]
    public string keyName;

    [SerializeField]
    public List<KeyCode> keyCodes = new List<KeyCode>();

    public KeyData(string keyname, List<KeyCode> keys)
    {
        keyName = keyname;
        keyCodes.AddRange(keys);
    }

    public string GetKeyName
    {
        get
        {
            return keyName;
        }

        set
        {
            keyName = value;
        }
    }

    public List<KeyCode> GetKeyCodes
    {
        get
        {
            return keyCodes;
        }

        set
        {
            keyCodes = value;
        }
    }
}

/// <summary>
/// キーコンフィグ情報を取り扱う
/// </summary>
public class KeyConfig
{
    
    private Dictionary<string, KeyData> config = new Dictionary<string, KeyData>();

    private readonly string configFilePath;

    /// <summary>
    /// キーコンフィグを管理するクラスを生成する
    /// </summary>
    /// <param name="configFilePath">コンフィグファイルのパス</param>
    public KeyConfig(string configFilePath)
    {
        this.configFilePath = configFilePath;
    }
    

    /// <summary>
    /// 指定したキーの入力状態をチェックする
    /// </summary>
    /// <param name="keyName">キーを示す名前文字列</param>
    /// <param name="predicate">キーが入力されているかを判定する述語</param>
    /// <returns>入力状態</returns>
    private bool InputKeyCheck(string keyName, Func<KeyCode, bool> predicate)
    {
        bool ret = false;
        foreach (var keyCode in config[keyName].GetKeyCodes)
            if (predicate(keyCode))
                return true;
        return ret;
    }

    /// <summary>
    /// 指定したキーが押下状態かどうかを返す
    /// </summary>
    /// <param name="keyName">キーを示す名前文字列</param>
    /// <returns>入力状態</returns>
    public bool GetKey(string keyName)
    {
        return InputKeyCheck(keyName, Input.GetKey);
    }

    /// <summary>
    /// 指定したキーが入力されたかどうかを返す
    /// </summary>
    /// <param name="keyName">キーを示す名前文字列</param>
    /// <returns>入力状態</returns>
    public bool GetKeyDown(string keyName)
    {
        return InputKeyCheck(keyName, Input.GetKeyDown);
    }

    /// <summary>
    /// 指定したキーが離されたかどうかを返す
    /// </summary>
    /// <param name="keyName">キーを示す名前文字列</param>
    /// <returns>入力状態</returns>
    public bool GetKeyUp(string keyName)
    {
        return InputKeyCheck(keyName, Input.GetKeyUp);
    }

    /// <summary>
    /// 指定されたキーに割り付けられているキーコードを返す
    /// </summary>
    /// <param name="keyName">キーを示す名前文字列</param>
    /// <returns>キーコード</returns>
    public List<KeyCode> GetKeyCode(string keyName)
    {
        if (config.ContainsKey(keyName))
            return new List<KeyCode>(config[keyName].GetKeyCodes);
        return new List<KeyCode>();
    }

    /// <summary>
    /// 名前文字列に対するキーコードを設定する
    /// </summary>
    /// <param name="keyName">キーに割り付ける名前</param>
    /// <param name="keyCode">キーコード</param>
    /// <returns>キーコードの設定が正常に完了したかどうか</returns>
    public bool SetKey(string keyName, List<KeyCode> keyCode)
    {
        if (string.IsNullOrEmpty(keyName) || keyCode.Count < 1)
            return false;
        config[keyName].GetKeyCodes = keyCode;
        return true;
    }

    /// <summary>
    /// コンフィグからキーコードを削除する
    /// </summary>
    /// <param name="keyName">キーに割り付けられている名前</param>
    /// <returns></returns>
    public bool RemoveKey(string keyName)
    {
        return config.Remove(keyName);
    }

    /// <summary>
    /// 設定されているキーコンフィグを確認用文字列として受け取る
    /// </summary>
    /// <returns>キーコンフィグを表す文字列</returns>
    public string CheckConfig()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var keyValuePair in config)
        {
            sb.AppendLine("Key : " + keyValuePair.Key);
            foreach (var value in keyValuePair.Value.GetKeyCodes)
                sb.AppendLine("  |_ Value : " + value);
        }
        return sb.ToString();
    }

    /// <summary>
    /// ファイルからキーコンフィグファイルをロードする
    /// </summary>
    public void LoadConfigFile()
    {
        //TODO:復号処理
        TextReader tr = new StreamReader(configFilePath, Encoding.UTF8);
        // try
        var str = tr.ReadToEnd();
        var pairs = JsonUtility.FromJson<Serialization<string, KeyData>>(str);
        config = pairs.ToDictionary();
    }

    /// <summary>
    /// 現在のキーコンフィグをファイルにセーブする
    /// </summary>
    public void SaveConfigFile()
    {
        Serialization<string, KeyData> serializationConfig = new Serialization<string, KeyData>(config);
        var jsonText = JsonUtility.ToJson(serializationConfig); ;

        using (TextWriter tw = new StreamWriter(configFilePath, false, Encoding.UTF8))
          tw.Write(jsonText);
    }
}