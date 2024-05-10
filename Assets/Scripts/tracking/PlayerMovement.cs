using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion q;
    public bool manual;
    [SerializeField] int playerIndex;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Esto no se llama si el tracking esta deshabilitado
    public void setPosition(Vector3 pos)
    {
        //swith playerIndex
        //transform.position = pos;

        /*Vector3 newPos;
        switch (playerIndex)
        {
            case 1:
                newPos = new Vector3(Mathf.Clamp(pos.x, 0, 100), pos.y, pos.z);
            break;

            case 2:
                newPos = new Vector3(Mathf.Clamp(pos.x, 0, 100), pos.y, pos.z);
            break;

            default:
                newPos = pos;
            break;
        }
        transform.position = newPos;*/
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }
}
