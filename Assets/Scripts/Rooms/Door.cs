using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private bool isVertical;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bool shouldEnterNextRoom = false;

            if (isVertical)
            {
                if (collision.transform.position.y < transform.position.y)
                {
                    shouldEnterNextRoom = true;
                } else
                {
                    shouldEnterNextRoom = false;
                }
            } else
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    shouldEnterNextRoom = true;
                } else
                {
                    shouldEnterNextRoom = false;
                }
            }

            if (shouldEnterNextRoom)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            } else
            {
                cam.MoveToNewRoom(previousRoom);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
