using System.Collections.Generic;

namespace solo_slasher.component.render;

public class RenderOperationsComponent : IComponent
{
    public Queue<IRenderOperation> RenderOperations = new();
}