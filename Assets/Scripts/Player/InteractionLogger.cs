using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class InteractionLogger : MonoBehaviour
{
    public static InteractionLogger Instance { get; private set; }
    private readonly string _logFilePath = Path.Combine(Application.streamingAssetsPath, "InteractionLog.txt");
    private readonly string _logFormat = "[{0}] {1} picked up a {2}";
    private DateTime _gameStartTime;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _gameStartTime = DateTime.Now;
        }
        else Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        ClearLogFile();
    }

    private string GetElaspedTime()
    {
        TimeSpan elapsedTime = DateTime.Now - _gameStartTime;
        return  elapsedTime.ToString(@"mm\:ss");
    }

    public void LogInteraction(string player, string gameObjectType)
    {
        try
        {
            string logEntry = string.Format(
                _logFormat,
                GetElaspedTime(),
                player,
                gameObjectType);

            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.Write(logEntry + Environment.NewLine);
                writer.Flush();
                writer.BaseStream.Flush();
            }

            Debug.Log($"Logged Interaction: {logEntry}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error writing to log file: {e.Message}");
        }
    }

    public void ClearLogFile()
    {
        try 
        {
            if (File.Exists(_logFilePath))
            {
                File.WriteAllText(_logFilePath, string.Empty);
                Debug.Log("Interaction log cleared for a new game");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error clearing log: {e.Message}");
        }
    }
}