using System;
using UnityEngine;

using Object = UnityEngine.Object;

[Serializable]
public class ResourceAsset<T> where T : Object
{
    [SerializeField] private string _assetPath = string.Empty;

    private T _loadedObject;

    public T Load()
    {
        return _loadedObject ??= Resources.Load<T>(_assetPath);
    }

    public void LoadAsync(Action<T> loaded = null)
    {
        if (_loadedObject != null)
        {
            loaded?.Invoke(_loadedObject);
            return;
        }

        ResourceRequest request = Resources.LoadAsync<T>(_assetPath);
        request.completed += OnRequestCompleted;

        void OnRequestCompleted(AsyncOperation operation)
        {
            var request = (ResourceRequest)operation;

            request.completed -= OnRequestCompleted;
            _loadedObject = (T)request.asset;
            loaded?.Invoke(_loadedObject);
        }
    }

    public static implicit operator T(ResourceAsset<T> resourceAsset)
    {
        return resourceAsset.Load();
    }
}
