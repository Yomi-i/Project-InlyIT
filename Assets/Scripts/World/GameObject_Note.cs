using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameObject_Note : MonoBehaviour
{
    // Variables.
    [SerializeField] private string _textFilePAth = "note_text.txt";
    private List<string> _noteLines = new List<string>();
    private string _noteText;


    // References.
    private UIManager _UImanager;

    
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

            _UImanager = UIManager.Instance;
            if (_UImanager != null)
            {
                _UImanager.ShowNote(_noteText);
            }
            else Debug.LogWarning("Unable to find UIManager instance");

            Destroy(gameObject);
            Debug.Log("Picked up a note.");
        }
    }
}