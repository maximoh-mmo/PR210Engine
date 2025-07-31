using OpenTK.Mathematics;

namespace Engine.Core.Camera
{
    public class CameraComponent : IComponent
    {
        private Vector3 _position;
        private Vector3 _target;
        private Vector3 _up;
        private float _fieldOfView;
        private float _nearPlane;
        private float _farPlane;
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; IsDirty = true; }
        }

        public Vector3 Target { get => _target; set { _target = value; IsDirty = true; } }
        public Vector3 Up { get => _up;  set { _up = value; IsDirty = true; } }
        public Vector3 Forward => Vector3.Normalize(Target - Position);
        public Vector3 Right => Vector3.Normalize(Vector3.Cross(Forward, Up));
        public float FieldOfView { get => _fieldOfView; set { _fieldOfView = value; IsDirty = true; } }
        public float NearPlane { get => _nearPlane; set { _nearPlane = value; IsDirty = true; } }
        public float FarPlane { get => _farPlane; set { _farPlane = value; IsDirty = true; } }
        public Matrix4 ViewMatrix { get; set; }
        public Matrix4 ProjectionMatrix { get; set; }

        public Matrix4 ViewProjectionMatrix { get; set; }
        public bool IsDirty { get; set; } = true;

        public CameraComponent()
        {
            IsDirty = true;
            Position = new Vector3(0,0,-5);
            Target = Vector3.Zero;
            Up = Vector3.UnitY;
            FieldOfView = 90.0f;
            NearPlane = 0.1f;
            FarPlane = 1000.0f;
            ViewMatrix = Matrix4.LookAt(Position, Target, Up);
        }
    }
}
