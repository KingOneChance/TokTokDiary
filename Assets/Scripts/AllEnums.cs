using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StickerType
{
    None,
    BubbleGunSticker,
    FreeSticker,
    RecordSticker,
    SignSticker,
    BubbleSticker,
    Diary
}

public enum ColorType
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Purple,
    Pink,
    Brown,
    Black,
    Eraser
}
public enum MouseType
{
    None,
    Niddle,
    BubbleStick,
}
public enum StickType
{
    None,
    Stick
}
public enum SoupBubbleType
{
    Soap,
    Attached
}
public enum CreateStickerState
{
    Select,
    Decide,
    Create
}
public enum SelectHedgehogState
{
    Body,
    Belly,
    Head,
    ArmLeg
}

public enum NowStateInAudio
{
    Make,
    Audio,
    Sign,
    Attatch,
    Done,
}
public enum AccessoryType
{
    Con,
    VolleyBall,
    HeadPalmtree,
    HeadBand,
    None
}
public enum SoundType
{ 
    BubbleBear, 
    BubbleSticker,
    BubbleGun,
    Diary,
    Common, 
    BGM,
    Touch
}