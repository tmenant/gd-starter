using Godot;

public static class MathUtils
{
    private const float pi = 3.1415926535897932f;

    private const float degToRad = pi / 180;

    public static float DegToRad(float degrees)
    {
        return degrees * degToRad;
    }

    public static float Clamp(float value, float minValue, float MaxValue)
    {
        if (value > MaxValue)
            return MaxValue;

        if (value < minValue)
            return minValue;

        return value;
    }
}