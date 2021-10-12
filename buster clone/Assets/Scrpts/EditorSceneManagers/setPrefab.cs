using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class setPrefab : MonoBehaviour
{

    [SerializeField]
    private GameObject _menuButton;


    private void Update()
    {
        if (_menuButton != null)
        {
            enabled = false;
            Button button = _menuButton.GetComponentInChildren<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(SceneChanger.SwitchToMenu);
            Debug.Log("here menu button");
        }
    }





}
