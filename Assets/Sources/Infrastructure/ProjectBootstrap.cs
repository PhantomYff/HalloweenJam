using UnityEngine;
using Zenject;

public class ProjectBootstrap : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        RegisterSceneLoader();

        //
        // ,,,
        //
    }

    private void RegisterSceneLoader()
    {
        Container.Bind<ISceneLoader>().FromInstance(_sceneLoader).AsSingle();
    }

    public override void Start()
    {
        _sceneLoader.Load(Scene.Main);
    }
}
