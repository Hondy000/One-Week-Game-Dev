using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

   
    /// <brief> カメラのターゲット.</brief>
    [SerializeField] private GameObject m_cameraTarget;

    [SerializeField] private Vector3 m_targetOffsetPosition; // ターゲット--とのオフセット位置

    /// <brief> ターゲットの位置.</brief>
    [SerializeField] private Camera m_camera;

    /// <brief> ターゲットまでの距離.</brief>
    [SerializeField] private float m_distance;

   
    [SerializeField] private Vector3 m_eulerRotation;  /// <brief> カメラ角度.</brief>

    // Use this for initialization
    private void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
        // カメラ位置の更新
        var quaternion = Quaternion.Euler(euler: m_eulerRotation);
        var rotateVector = quaternion * new Vector3(x: 0, y: 0, z: m_distance);
        var position = m_cameraTarget.transform.position;
        m_camera.transform.position = position + m_targetOffsetPosition + rotateVector;
        
        m_camera.transform.LookAt(position + m_targetOffsetPosition, worldUp: new Vector3(x: 0, y: 1, z: 0));

    }
}
