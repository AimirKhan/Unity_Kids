using UnityEngine;

namespace Services.Save
{
    [System.Serializable]
    public class SaveData
    {
        public string Version = "1.0";
    }

    [System.Serializable]
    public struct ColorDto
    {
        public float r, g, b, a;
        
        public ColorDto(Color c) {  r = c.r; g = c.g; b = c.b; a = c.a; }
        public Color ToColor() => new(r, g, b, a);
    }
}