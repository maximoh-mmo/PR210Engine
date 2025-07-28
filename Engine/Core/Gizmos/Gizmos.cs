using Engine.Core.Systems;

namespace Engine.Core.Gizmos
{
    static class Gizmos
    {
        static GizmoSystem _gizmoSystem;
        static void Initialize(GizmoSystem? gizmoSystem = null)
        {
            if (gizmoSystem != null)
            {
                _gizmoSystem = gizmoSystem;
                return;
            }
            // Initialize the Gizmo system
            _gizmoSystem = new GizmoSystem();
        }

        static void DrawSphere()
        {
        }
    }
}
