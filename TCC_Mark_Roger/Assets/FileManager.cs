using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class FileManager
{

    public static void SaveGame(GameSaveData saveData, int slot)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save"+slot.ToString(), FileMode.Create);
        bf.Serialize(stream, saveData);
        stream.Close();
        Debug.Log("The Game Was Saved in the Slot: "+ slot);
    }


    public static void SaveSettings(GameSettingsData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gamesettings", FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();
        Debug.Log("Game Settings Saved");
    }

    public static GameSaveData LoadGame(int slot)
    {
        if (File.Exists(Application.persistentDataPath + "/save"+slot.ToString()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/save" + slot.ToString(), FileMode.Open);

            GameSaveData data = bf.Deserialize(stream) as GameSaveData;

            stream.Close();
            Debug.Log("Loaded Game from Slot: "+ slot);
            return data;

        }
        else
        {
            Debug.Log("No Savefile Found. ");
            return new GameSaveData();
        }

    }

    public static GameSettingsData LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesettings"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/settings.roy", FileMode.Open);
            GameSettingsData data = bf.Deserialize(stream) as GameSettingsData;
            stream.Close();
            Debug.Log("Game Settings Loaded");
            return data;
        }
        else
        {
            Debug.Log("No SettingsFile Founded");
            return new GameSettingsData();
        }
    }
}

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////
/// </summary>
/// 

[Serializable]
public struct ObjectiveData
{
    public int id;
    public bool enabled;
    public bool completed;


}

[Serializable]
public struct NPCData {
    public int id;
    public bool enabled;
    public int nPCStateId;
}

[Serializable]
public class Vector3Serial
{
    public float x, y, z;

    public Vector3Serial() {
        x = 0;
        y = 0;
        z = 0;
    }
    public Vector3Serial(Vector3 v) {
        x = v.x;
        y = v.y;
        z = v.z;
    }
}


[Serializable]
public class GameSaveData
{
    [SerializeField]
    public Vector3Serial playerPosition;
    public List<ObjectiveData> objectivesData;
    public List<NPCData> npcsData;


    public GameSaveData()
    {
        playerPosition = new Vector3Serial();
        objectivesData = new List<ObjectiveData>();
        npcsData = new List<NPCData>();

    }
}

[Serializable]
public class GameSettingsData
{
    public float musicVolume;
    public float sfxVolume;

    public GameSettingsData()
    {
        musicVolume = 0.8f;
        sfxVolume = 0.8f;
    }


}