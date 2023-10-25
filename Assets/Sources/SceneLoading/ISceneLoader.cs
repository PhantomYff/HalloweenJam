using System;

public interface ISceneLoader
{
    void Load(string scene, Action<float> onProgress = null, Action completed = null);
}
