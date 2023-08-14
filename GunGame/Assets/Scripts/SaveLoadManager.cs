using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
class SaveData
{
    public Dictionary<int, string> dataScore = new Dictionary<int, string>();
}

public class SaveLoadManager : MonoBehaviour
{
    string fileResources = "Assets/Resources";
    string nameFileCredits = "/Credits.csv";
    string nameFileTop = "/TopScores.save";
    string dateFormat = "dd MMMM, yyyy";

    public string dataString { get; private set; }
    public string creditString { get; private set; }
    public int score { get; set; }

    public Dictionary<int, string> dataScore = new Dictionary<int, string>();

    void Start()
    {
        //LoadText(nameFileCredits);

        LoadProgress();

        SaveProgress();

    }



    private async void LoadText(string name)
    {
        
        using (FileStream fstream = File.OpenRead(fileResources + name))
        {
            byte[] buffer = new byte[fstream.Length];
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            creditString = Encoding.Default.GetString(buffer);
        }
    }

    public void SaveProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + nameFileTop);
        SaveData data = new SaveData();

        dataString = " : " + DateTime.Now.ToString(dateFormat);

        if (dataScore.ContainsKey(score)) dataScore[score] = dataString;
        else dataScore.Add(score, dataString);
        data.dataScore = dataScore;

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadProgress()
    {

        if (File.Exists(Application.persistentDataPath + nameFileTop))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + nameFileTop, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            dataScore = data.dataScore;

        }
        else
        {
            SaveProgress();
        }

        
    }
}
