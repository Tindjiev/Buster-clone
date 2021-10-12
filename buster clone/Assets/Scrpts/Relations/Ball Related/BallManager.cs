using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

[ExecuteInEditMode]
public class BallManager : MonoBehaviour, IEnumerable<BallBase>
{
    [SerializeField]
    private GameObject _ballPrefab;
    [SerializeField]
    private BallSO _defaultSO;


    private List<BallBase> _balls = new List<BallBase>(30);

    [SerializeField]
    [Inject]
    private DropManager _dropManager;

    public int BallCount => transform.childCount;

    private void Awake()
    {

        _balls.Clear();
        _balls.AddRange(GetComponentsInChildren<BallBase>());
    }

    public void AddBall(BallBase ball)
    {
        _balls.Add(ball);
    }


    public IEnumerator<BallBase> GetEnumerator()
    {
        _balls.RemoveAll(x => x == null);
        return _balls.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }



#if UNITY_EDITOR

    public BallType AddBall<BallType>(BallSO ballInfo, Vector3 position) where BallType : BallBase
    {
        _ballPrefab.SetActive(false);

        BallType newBall = (PrefabUtility.InstantiatePrefab(_ballPrefab, transform) as GameObject).AddComponent<BallType>();
        newBall.transform.position = position;

        newBall.gameObject.name = newBall.gameObject.name.Replace("(Clone)", string.Empty);
        newBall.SetBallInfo(_defaultSO);
        newBall.Construct(this, _dropManager);

        AddBall(newBall);

        newBall.gameObject.SetActive(true);
        _ballPrefab.SetActive(true);
        return newBall;
    }

    [UnityEditor.MenuItem("GameObject/Balls/Basic Ball", false, 0)]
    private static void EditorAddBasicBall()
    {
        BallManager bm = FindObjectOfType<BallManager>();
        GameObject ball = bm.AddBall<BasicBall>(null, Vector3.zero).gameObject;
        UnityEditor.Undo.RegisterCreatedObjectUndo(ball, "destroy ball that was created");
        Selection.objects = new Object[] { ball };
    }

#endif


}
