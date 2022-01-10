using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerData : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject = null;
    [SerializeField] Image toggle = null;

    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle = null;
    [SerializeField] Sprite checkedToggle = null;

    private int _answerIndex = -1;
    public int AnswerIndex { get { return _answerIndex; } }
}
