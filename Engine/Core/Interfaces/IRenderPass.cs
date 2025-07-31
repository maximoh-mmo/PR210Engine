// // ------------------------------------------------------------------
// //       Copyright (c) Max Heinze 2024. All Rights Reserved.
// // ------------------------------------------------------------------

using Engine.Core.Camera;

namespace Engine.Core.Interfaces;

public interface IRenderPass
{
    public string Name { get; }
    public bool Enabled { get; set; }
    public IReadOnlyList<string> Dependencies { get; }

    public void Execute(IReadOnlyList<GameObject> gameObjects, CameraComponent camera);
}