using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveController : MonoBehaviour
{
    public static SaveController instance;
    public saveData save;
    private saveData defaultInfo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            defaultInfo = save;
            loadInfo();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadInfo()
    {
        string dataPath=Application.persistentDataPath;
        if(File.Exists(dataPath+"/save.date"))
        {
            var serializer = new XmlSerializer(typeof(saveData));
            var stream = new FileStream(dataPath + "/save.data", FileMode.Open);
            save=serializer.Deserialize(stream)as saveData;
            stream.Close();
            Debug.Log("data load");
        }
    }
    public void saveInfo()
    {
        string dataPath=Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(saveData));
        var stream = new FileStream(dataPath + "/save.data", FileMode.Create);
        serializer.Serialize(stream, save);
        stream.Close();
        Debug.Log("data save");

    }
    private void OnApplicationQuit()
    {
        saveInfo();
    }
    public void restsave()
    {
        save = defaultInfo;
    }
    public void markProgress(string progressMark)
    {
        bool found = false;
        for(int i = 0; i < save.progress.Count; i++)
        {
            if(save.progress[i].name == progressMark)
            {
                found = true;   
                save.progress[i].isMark = true;
                i=save.progress.Count;
            }
        }
        if(!found)
        {
            Debug.Log("couldn't found" + progressMark);
        }
    }
    public bool CheckProgress(string Tocheck)
    {
        bool isMark=false;
        for (int i = 0; i < save.progress.Count; i++)
        {
            if (save.progress[i].name == Tocheck)
            {
                isMark = save.progress[i].isMark;
                
                i = save.progress.Count;
            }
        }
        return isMark;
    }
}
[System.Serializable]
public class saveData
{
    public bool hasBegun;
    public Vector3 startPosition;
    public string curScene;
    public int maxHP, curSword, swordDamage, curCoin;
    public float sta;
    public List<progressIT>progress=new List<progressIT>();
}
[System.Serializable]
public class progressIT
{
    public string name;
    public bool isMark;
}
