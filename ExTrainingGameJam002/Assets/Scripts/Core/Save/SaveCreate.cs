using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Player;
using TimerSystem;
using StageSystem;

public class SaveCreate : MonoBehaviour
{
    public GameObject player;
    public AreaClear areaClear;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public TimerController timer;
    public CameraMove cm;

    public void SaveGame()
    {
        // セーブしたい情報を取得してSaveDataクラスに格納
        SaveData saveData = new SaveData();
        saveData.playerPositionX = player.transform.position.x;
        saveData.playerPositionY = player.transform.position.y;
        saveData.HaveKey = areaClear.HaveKey;

        saveData.enemy1PositionX = enemy1.transform.position.x;
        saveData.enemy1PositionY = enemy1.transform.position.y;
        saveData.enemy2PositionX = enemy2.transform.position.x;
        saveData.enemy2PositionY = enemy2.transform.position.y;
        saveData.enemy3PositionX = enemy3.transform.position.x;
        saveData.enemy3PositionY = enemy3.transform.position.y;

        saveData.Timer = timer.currentTime;

        saveData.AreaPointIndexNum = cm.currentPointIndex;

        // 他の情報もここでセット

        // SaveDataをバイナリ形式にシリアライズして保存
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedata.dat");
        bf.Serialize(file, saveData);
        file.Close();
    }
}