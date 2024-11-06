using System.IO;
using System.Collections.Generic;

using UnityEngine;

public static class FileHandler
{
    public static void SaveToJSON<T>(T data, string fileName = "LocalData.json")
    {
        string filePath = GetFilePath(fileName);
        Debug.Log(filePath);
        string serializedData = JsonHelper.ToJson<T>(data);
        WriteToFile(filePath, serializedData);
    }

    public static void SaveToJSON<T>(List<T> listToSave, string fileName)
    {
        string filePath = GetFilePath(fileName);
        Debug.Log(filePath);
        string serializedData = JsonHelper.ToJson<T>(listToSave.ToArray(), true);
        WriteToFile(filePath, serializedData);
    }

    private static string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    private static void WriteToFile(string filePath, string contentToSave)
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create); // 새 파일스트림 생성. 만약 이미 존재한다면 덮어쓰기

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(contentToSave);
        }
    }

    public static string ReadToFile(string fileName)
    {
        string filePath = GetFilePath(fileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        using (StreamReader reader = new StreamReader(fileStream))
        {
            return reader.ReadToEnd();
        }
    }
}