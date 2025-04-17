using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    //Variables.
    [SerializeField] private KeyCode _closeNoteKey;
    [SerializeField] private GameObject _notePanel;
    [SerializeField] private Text _noteText;
    [SerializeField] private Button _logButton;


    // flags.
    private bool bIsNoteOpen = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _notePanel.SetActive(false);
        }
        else Destroy(gameObject);
    }

        private void Update()
    {
        if (bIsNoteOpen)
        {
            if (Input.GetKeyDown(_closeNoteKey))
            {
                CloseNote();
            }
        }
    }

    public void ShowNote(string text)
    {
        if (bIsNoteOpen) CloseNote();
        _noteText.text = text;
        _notePanel.SetActive(true);
        bIsNoteOpen = true;
    }

    public void ShowLogNote()
    {
        if (bIsNoteOpen) _noteText.text = null;
        _noteText.text = InteractionLogger.Instance.ReadLogFile();
        _notePanel.SetActive(true);
        _logButton.gameObject.SetActive(false);
        bIsNoteOpen = true;
    }

    public void CloseNote()
    {
        _notePanel.SetActive(false);
        _noteText.text = null;
        _logButton.gameObject.SetActive(true);
        bIsNoteOpen = false;
    }
}