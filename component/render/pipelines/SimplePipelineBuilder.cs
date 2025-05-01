using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace solo_slasher.component.render.pipelines;

public static class SimplePipelineBuilder
{
    public static RenderPipelineComponent Build(IRenderOperation operation)
    {
        return new RenderPipelineComponent(Pipeline);

        IEnumerable<IRenderOperation> Pipeline(GameTime gameTime, Entity entity)
        {
            yield return operation;
        }
    }
}