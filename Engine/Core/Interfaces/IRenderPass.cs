// // ------------------------------------------------------------------
// //       Copyright (c) Max Heinze 2024. All Rights Reserved.
// // ------------------------------------------------------------------

namespace Engine.Core.Interfaces;

public interface IRenderPass
{
    public string Name { get; }
    public bool Enabled { get; set; }
    public IReadOnlyList<string> Dependencies { get; }

    public void Execute(List<GameObject> gameObjects, float aspectRatio);
}