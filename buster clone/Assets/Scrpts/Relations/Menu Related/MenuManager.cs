using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuManager : MonoBehaviour
{
    private Transform _buttonsParent;
    private GameObject _controlsParent;
    private Button[] buttons;
    private TextMeshProUGUI[] buttonsText;

    [Inject]
    public void Construct([Inject(Id = ZenjectId.StageSelect)] RectTransform buttonsParent, [Inject(Id = ZenjectId.ControlsGui)] RectTransform controlsParent)
    {
        _buttonsParent = buttonsParent;
        _controlsParent = controlsParent.gameObject;
    }


    private void Awake()
    {
        SetButtonsNumber();
        SetButtonsFunctionality();
        //_controlsButton.onClick.AddListener();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OpenControls()
    {
        _controlsParent.SetActive(true);
    }
    public void CloseControls()
    {
        _controlsParent.SetActive(false);
    }


    private void SetButtonsNumber()
    {
        Transform buttonTr = _buttonsParent.GetChild(0);
        for (int i = 1; i < SceneChanger.StageCount; i++)
        {
            Instantiate(buttonTr, buttonTr.parent);
        }
        buttons = _buttonsParent.GetComponentsInChildren<Button>();
        buttonsText = _buttonsParent.GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void SetButtonsFunctionality()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].onClick.AddListener(new StageSelect(i).SelectStage);
        }
        for (int i = 0; i < buttonsText.Length; ++i)
        {
            buttonsText[i].text = "stage " + i;
        }
    }


    private class StageSelect
    {

        private readonly int _stageNumber;

        public StageSelect(int stageNumber)
        {
            _stageNumber = stageNumber;
        }

        public void SelectStage()
        {
            SceneChanger.SwitchToStage(_stageNumber);
        }
    }


}
