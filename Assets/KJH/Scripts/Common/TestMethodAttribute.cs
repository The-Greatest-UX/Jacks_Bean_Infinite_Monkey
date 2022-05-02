using System;

public class TestMethodAttribute : Attribute
{
    public bool isEnableNotPlaying;
    public TestMethodAttribute(bool inPausePlay = true)
    {
        this.isEnableNotPlaying = inPausePlay;
    }
}
