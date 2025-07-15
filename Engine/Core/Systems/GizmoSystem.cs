using Engine.Core.Services;

namespace Engine.Core.Systems
{
    public class GizmoSystem
    {
        private Queue<GizmoDrawCommand> _frameGizmos;
        private List<GizmoDrawCommand, float> _timedGizmos;

        public void AddGizmo(GizmoDrawCommand gizmoDrawCommand)
        {
            _timedGizmos.Add(gizmoDrawCommand);
        }

        public void Update(float deltaTime)
        {
            // Update timed gizmos
            for (int i = _timedGizmos.Count - 1; i >= 0; i--)
            {
                var timedGizmo = _timedGizmos[i];
                timedGizmo.TimeLeft -= deltaTime;
                if (timedGizmo.TimeLeft <= 0)
                {
                    _timedGizmos.RemoveAt(i);
                }
            }
            // Process frame gizmos
            while (_frameGizmos.Count > 0)
            {
                var gizmo = _frameGizmos.Dequeue();
                // Render or process the gizmo
            }
        }

        public void Render()
        {

        }
    }
}
