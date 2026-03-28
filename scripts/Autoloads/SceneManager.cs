using System;
using Godot;


public partial class SceneManager : Node
{
    private static readonly Logging.Logger logger = Logging.CreateLogger<SceneManager>();

    public static SceneManager Instance { get; set; }

    private CanvasLayer UIRoot;

    public SceneManager()
    {
        if (Instance != null)
        {
            throw new Exception("Duplicate SceneManager");
        }

        Instance = this;
    }

    public static T InstantiateScene<T>(string path) where T : Node
    {
        PackedScene packedScene = GD.Load<PackedScene>(path);

        if (packedScene != null)
        {
            return packedScene.Instantiate() as T;
        }

        logger.Error($"Loading scene failed : '{path}'");

        return null;
    }

    public T LoadScene<T>(string path, Node parent = null) where T : Node
    {
        if (parent == null)
            parent = this;

        if (InstantiateScene<T>(path) is Node node)
        {
            parent.AddChild(node);

            return node as T;
        }

        return null;
    }

    public void LoadScene(string path, Node parent = null)
    {
        LoadScene<Node>(path, parent);
    }

    public T LoadUIController<T>(Control parent) where T : Control
    {
        return LoadScene<T>($"res://scenes/UI/{typeof(T).Name}.tscn", parent);
    }

    public T LoadUIController<T>() where T : Control
    {
        return InstantiateScene<T>($"res://scenes/UI/{typeof(T).Name}.tscn");
    }
}
