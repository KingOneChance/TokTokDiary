using UnityEngine;

public static class Manager_UserInput
{
    public static int touchCount = 0;
    public static Touch[] touches = new Touch[10];

    public static void UpdateTouch()
    {
        if(Input.touchCount > 0)
        {
            touchCount = Input.touchCount;
            touches[0] = Input.GetTouch(0);
        }
    }
}
