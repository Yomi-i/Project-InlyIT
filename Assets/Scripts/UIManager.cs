using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour,
IPointerUpHandler
{
    public static UIManager Instance { get; private set; }


    //Variables.
    [SerializeField] private KeyCode _closeNoteKey;
    [SerializeField] private GameObject _notePanel;
    [SerializeField] private Text _noteText;


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

    public void ShowNote(string text)
    {
        if (bIsNoteOpen) CloseNote();
        _noteText.text = text;
        _notePanel.SetActive(true);
        bIsNoteOpen = true;
    }

    public void CloseNote()
    {
        _notePanel.SetActive(false);
        _noteText.text = null;
        bIsNoteOpen = false;
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

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
