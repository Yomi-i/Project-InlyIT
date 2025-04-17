using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameObject_Note : MonoBehaviour
{
    // Variables.
    private readonly string _textFilePAth = "note_text.txt";
    private List<string> _noteLines = new List<string>();
    private string _noteText;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, _textFilePAth);
            string [] lines = File.ReadAllLines(fullPath);

            _noteLines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

            if (_noteLines.Count > 0)
            {
                _noteText = _noteLines[Random.Range(0, _noteLines.Count)];
            }
            else
            {
                Debug.LogWarning("No valid lines found in note text file");
                _noteText = "...";
            }

            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowNote(_noteText);
            }
            else Debug.LogWarning("Unable to find UIManager instance");

            Destroy(gameObject);

            if (InteractionLogger.Instance != null) InteractionLogger.Instance.LogInteraction("Player", $"Note with writing: {_noteText}");
            Debug.Log("Picked up a note.");
        }
    }
}