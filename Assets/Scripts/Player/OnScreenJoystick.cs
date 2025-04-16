
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnScreenJoystick : MonoBehaviour,
IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private float _joystickOffset = 2.3f;

    // References.
    private Image _bgJoystickImage;
    private Image _joystickKnob;
    public Vector2 _InputDir {set; get;}

    // Start is called before the first frame update
    void Start()
    {
        _bgJoystickImage = GetComponent<Image>();
        _joystickKnob = transform.GetChild(0).GetComponent<Image>();
        _InputDir = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        float bgImageSizeX = _bgJoystickImage.rectTransform.sizeDelta.x;
        float bgImageSizeY = _bgJoystickImage.rectTransform.sizeDelta.y;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _bgJoystickImage.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position)) 
            {
                position.x /= bgImageSizeX;
                position.y /= bgImageSizeY;
                _InputDir = new Vector2(position.x, position.y);
                _InputDir = _InputDir.magnitude > 1 ? _InputDir.normalized : _InputDir;

                _joystickKnob.rectTransform.anchoredPosition = 
                new Vector2(_InputDir.x * (bgImageSizeX / _joystickOffset),
                            _InputDir.y * (bgImageSizeY / _joystickOffset));
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _InputDir = Vector2.zero;
        _joystickKnob.rectTransform.anchoredPosition = Vector2.zero;
    }
}
