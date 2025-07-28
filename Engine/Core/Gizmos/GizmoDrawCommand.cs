using OpenTK.Mathematics;

namespace Engine.Core.Gizmos
{
    public class GizmoDrawCommand
    {
        public GizmoType type;
        public Vector3 position;
        public Vector3 scale;
        public Vector4 colour;
        public Vector3? endPosition;
        public float duration;
    }

    public enum GizmoType
    {
        Sphere,
        Cube,
        Line,
        Ray,
        WireframeCube,
        WireframeSphere,
    }
}
