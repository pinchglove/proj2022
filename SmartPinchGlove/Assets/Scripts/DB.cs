using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.Networking;

public class DB : MonoBehaviour
{
    static IDbConnection dbConnection;
    static IDbCommand dbCommand;
    public static IDataReader dataReader;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        DBConnectionCheck();
    }

    private void Update()
    {
 
    }
    /*
    static IEnumerator DBCreate()
    {
        string filepath = string.Empty;
        filepath = Application.dataPath + "/DataBase.db";
        if(!File.Exists(filepath))
        { 
            File.Copy(Application.streamingAssetsPath + "/DataBase.db", filepath);
            yield return null;
        }
        Debug.Log("DB파일생성 완료");
    }
    */
    public static string GetDBFilePath()
    {
        string str = string.Empty;
        //str = "URI=File:" + Application.dataPath + "/DataBase.db";
        //str = "URI=File:" + Application.dataPath + "/StreamingAssets/DataBase.db";
        str = "URI=File:" + Application.streamingAssetsPath + "/DataBase.db";

        return str;
    }
    
    public static void DBConnectionCheck()
    {
        try
        {
            dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if(dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("DB연결 성공");
            }
            else
            {
                Debug.Log("연결실패(에러)");
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        dbConnection.Close();
    }

    public static void DataBaseRead(string query)
    {
        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        dataReader = dbCommand.ExecuteReader();

      
        /*while (dataReader.Read())
        {
            Debug.Log(dataReader.GetInt32(0)); // + ", " + dataReader.GetString(1) + ", " + dataReader.GetString(2));
        }
        */
    }

    public static void DatabaseInsert(string query)
    {
        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }


    public static void DBClose()
    {
        //생성 순서 반대로 닫기
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
