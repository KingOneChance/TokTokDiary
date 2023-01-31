using UnityEngine;

public static class Manager_UserInput
{
    public static UserInputState curInputState;

    public static void UpdateTouch()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    curInputState = UserInputState.TouchBegan;
                    break;

                case TouchPhase.Stationary:
                    curInputState = UserInputState.TouchStationary;
                    break;

                case TouchPhase.Canceled:
                    curInputState = UserInputState.TouchCanceled;
                    break;

                case TouchPhase.Moved:
                    curInputState = UserInputState.TouchMoved;
                    break;

                case TouchPhase.Ended:
                    curInputState = UserInputState.TouchEnded;
                    break;
            }
        }
    }
}
