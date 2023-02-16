using UnityEngine;

[CreateAssetMenu(fileName = "New ClipsDataObj", menuName = "ScriptableObjects/AudioClipsDataObj", order = 1)]

public class Data_Audios : ScriptableObject
{
    public SoundType soundType;
    public AudioClip[] Sounds;
}
