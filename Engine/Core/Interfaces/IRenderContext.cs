// // ------------------------------------------------------------------
// //       Copyright (c) Max Heinze 2024. All Rights Reserved.
// // ------------------------------------------------------------------

namespace Engine.Core.Interfaces;

public interface IRenderContext
{
    int DefaultFboId { get; }
    void BindFramebuffer(int fboId);
}