using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool manual;
    public Quaternion q;

    public bool selectingPlayer = false;

    [SerializeField] int playerIndex;
    void Start()
    {
        selectingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Esto no se llama si el tracking esta deshabilitado
    public void setPosition(Vector3 pos)
    {
        //swith playerIndex
        transform.position = pos;
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }
}
