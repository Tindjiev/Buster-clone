using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFPS : MonoBehaviour
{
    [SerializeField]
    private int _maxFPS = 60;

    private GUIStyle _style = new GUIStyle();

    private float _avg0 = 0f;
    private float _avg1 = 0f;

    private readonly Transform[] _walls = new Transform[4];
    private Transform _background;
    private Camera _camera;

    [Inject]
    public void Construct([Inject(Id = ZenjectId.Walls)] Transform walls,
        [Inject(Id = ZenjectId.Background)] Transform background,
        [Inject(Id = ZenjectId.MainCamera)] Camera mainCamera)
    {
        for(int i = 0; i < _walls.Length; i++)
        {
            _walls[i] = walls.GetChild(i);
        }
        _background = background;
        _camera = mainCamera;
    }



    private void Awake()
    {
        _style.alignment = TextAnchor.UpperLeft;
        _style.normal.textColor = Color.gray;
        _style.fontSize = 20;
        Application.targetFrameRate = _maxFPS;
    }


    void OnGUI()
    {
        //_style.fontSize = Screen.width / 100;
        _avg0 += ((Time.deltaTime / Time.timeScale) - _avg0) * 0.03f;
        _avg1 += (_avg0 - _avg1) * 0.03f;
        GUI.Label(new Rect(0f, 0f, 0f, 0f), string.Format("{0:0.0} ms ({1} fps)", _avg1 * 1000f, (int)(1f / _avg1 + 0.5f)), _style);
    }





}
