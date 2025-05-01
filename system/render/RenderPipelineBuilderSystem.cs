using Microsoft.Xna.Framework;
using solo_slasher.component.render;

namespace solo_slasher.system.render;

public class RenderPipelineBuilderSystem
{
    public void Update(GameTime gameTime)
    {
        foreach (var entity in EntityManager.GetEntitiesWith<RenderPipelineComponent>())
        {
            if (!EntityManager.HasComponent<RenderOperationsComponent>(entity))
            {
                EntityManager.AddComponent(entity, new RenderOperationsComponent());
            }

            var pipeline = EntityManager.GetComponent<RenderPipelineComponent>(entity);
            var operations = EntityManager.GetComponent<RenderOperationsComponent>(entity);
            operations.RenderOperations.Clear();
            foreach (var operation in pipeline.BuildPipeline(gameTime, entity))
            {
                operations.RenderOperations.Enqueue(operation);
            }
        }
    }
}