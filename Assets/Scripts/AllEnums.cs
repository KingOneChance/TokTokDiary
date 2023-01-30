using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StickerType
{
    BubbleGunSticker,
    FreeSticker,
    AudioSticker,
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
public enum NiddleType
{
    None,
    Niddle
}
public enum SoupBubbleType
{
    Soap,
    Attached
}
public enum StickType
{
    None,
    Stick
}
public enum CreateStickerState
{
    DecideDesign,
    SelectColor,
    Stir
}
public enum SelectHedgehogState
{
    Body,
    Belly,
    Head,
    ArmLeg
}
public enum UserInputState
{
    TouchBegan,
    TouchStationary,
    TouchMoved,
    TouchEnded,
    TouchCanceled
}