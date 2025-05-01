using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace solo_slasher.component.render;

public class RenderPipelineComponent(Func<GameTime, Entity, IEnumerable<IRenderOperation>> buildPipeline) : IComponent
{
    public readonly Func<GameTime, Entity, IEnumerable<IRenderOperation>> BuildPipeline = buildPipeline;
}