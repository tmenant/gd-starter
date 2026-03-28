using System.Collections.Generic;
using System.IO;
using Godot;

public static class GDUtils
{
    public static bool MouseCaptured => Input.MouseMode == Input.MouseModeEnum.Captured;

    public static T FindChild<T>(Node root) where T : Node
    {
        foreach (Node child in root.GetChildren())
        {
            if (child is T)
            {
                return child as T;
            }

            if (FindChild<T>(child) is T node)
            {
                return node;
            }
        }

        return null;
    }

    public static T FindChild<T>(Node root, string name) where T : Node
    {
        foreach (Node child in root.GetChildren())
        {
            if (child is T && child.Name == name)
            {
                return child as T;
            }

            if (FindChild<T>(child, name) is T node && node.Name == name)
            {
                return node;
            }
        }

        return null;
    }

    public static IEnumerable<T> GetChildren<T>(Node parent)
    {
        foreach (var child in parent.GetChildren())
        {
            if (child is T validChild)
            {
                yield return validChild;
            }

            foreach (var recursiveChild in GetChildren<T>(child))
            {
                yield return recursiveChild;
            }
        }
    }

    public static void SetMouseMode(Input.MouseModeEnum mouseMode)
    {
        Input.MouseMode = mouseMode;
    }

    public static bool IsChildOf(Node child, Node parent)
    {
        Node current = child.GetParent();

        while (current != null)
        {
            if (current == parent)
                return true;

            current = current.GetParent();
        }

        return false;
    }

    public static T FindParent<T>(Node child) where T : Node
    {
        Node current = child.GetParent();

        while (current != null)
        {
            if (current is T)
                return current as T;

            current = current.GetParent();
        }

        return null;
    }

    public static bool FileExists(string gdPath)
    {
        return File.Exists(ProjectSettings.GlobalizePath(gdPath));
    }

}