using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{

    public GUIStyle clockstyle;

    private float _timer, _minitus, _seconds;

    private const float virtualwidth = 480.0f;
    private const float virtualHight = 854.0f;
    private bool _stoptimer;
    private Matrix4x4 _matrix;
    private Matrix4x4 _oldMatrix;

    // Start is called before the first frame update
    void Start()
    {
        _stoptimer = false;
        _matrix = Matrix4x4.TRS(pos: Vector3.zero, Quaternion.identity, s: new Vector3(x: Screen.width / virtualwidth, y: Screen.height / virtualHight, z: 1.0f));
        _oldMatrix = GUI.matrix;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stoptimer)
            _timer += Time.deltaTime;
    }

    private void OnGUI()
    {
        GUI.matrix = _matrix;
        _minitus = Mathf.Floor(f: _timer / 60);
        _seconds = Mathf.RoundToInt(f: _timer % 60);
        GUI.Label(position: new Rect(x: Camera.main.rect.x + 20, y: 10, width: 120, height: 50), text: "" + _minitus.ToString(format: "00") + ":" + _seconds.ToString(format: "00"), clockstyle);
        GUI.matrix = _oldMatrix;
    }
    public float GetCurrentTime()
    {

        return _timer;

     }
    public void Stoptimer()
    {
        _stoptimer = true;
    }
}
