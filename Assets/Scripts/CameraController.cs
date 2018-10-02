using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

   
    /// <brief> カメラのターゲット.</brief>
    [SerializeField]
    GameObject m_cameraTarget;

    /// <brief> ターゲットの位置.</brief>
    [SerializeField]
    Camera m_camera;

    /// <brief> ターゲットまでの距離.</brief>
    [SerializeField]
    float m_distance;

   
    [SerializeField]
    Vector3 m_eulerRotation;  /// <brief> カメラ角度.</brief>

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // カメラ位置の更新
        var _quaternion = Quaternion.Euler(m_eulerRotation);
        var _rotateVector = _quaternion * new Vector3(0, 0, m_distance);
        m_camera.transform.position = m_cameraTarget.transform.position + _rotateVector;

        m_camera.transform.localRotation = _quaternion;

    }
}
