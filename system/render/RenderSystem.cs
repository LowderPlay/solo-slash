using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.system.render;

public class RenderSystem(SpriteBatch spriteBatch, Rectangle screenSize)
{
    private readonly RenderTarget2D _gameScreen = new (spriteBatch.GraphicsDevice, screenSize.Width, screenSize.Height);
    
    public void Render(Rectangle viewportBounds, GameTime gameTime)
    {
        foreach (var entity in EntityManager.GetEntitiesWith<InternalCanvasComponent>())
        {
            var canvasComponent = EntityManager.GetComponent<InternalCanvasComponent>(entity);
            
            canvasComponent.Target ??= new RenderTarget2D(spriteBatch.GraphicsDevice, canvasComponent.Size.X, canvasComponent.Size.Y);
            
            spriteBatch.GraphicsDevice.SetRenderTarget(canvasComponent.Target);
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);
            canvasComponent.InternalRender.Invoke(spriteBatch, entity);
            spriteBatch.End();
            
        }
        
        spriteBatch.GraphicsDevice.SetRenderTarget(_gameScreen);
        spriteBatch.GraphicsDevice.Clear(new Color(95, 162, 69));
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        var cameraPosition = Vector2.Zero;
        if (EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var camera) && 
            EntityManager.TryGetComponent<PositionComponent>(camera, out var cameraPositionComponent))
        {
            cameraPosition = cameraPositionComponent.Position;
        }
        
        var screenOrigin = new Vector2(screenSize.Width / 2f, screenSize.Height / 2f);

        foreach (var entity in EntityManager.GetEntitiesWith<RenderOperationsComponent>()
                     .OrderBy(e => EntityManager.TryGetComponent<ZOrderComponent>(e, out var z) ? z.ZOrder : 0)
                     .ThenBy(e => EntityManager.TryGetComponent<PositionComponent>(e, out var pos) ? 
                         pos.Position.Y : 0)
                     .ThenBy(e => EntityManager.TryGetComponent<ScreenPositionComponent>(e, out var pos) ? 
                         pos.Position.Y : 0))
        {
            if (EntityManager.HasComponent<HiddenComponent>(entity)) continue;
            var drawPosition = EntityManager.TryGetComponent<ScreenPositionComponent>(entity, out var pos) ? 
                pos.Position : screenOrigin;

            if (!EntityManager.HasComponent<CameraOriginComponent>(entity) && 
                EntityManager.TryGetComponent<PositionComponent>(entity, out var positionComponent))
            {
                drawPosition += positionComponent.Position - cameraPosition;
            }
            
            if (!screenSize.Intersects(new Rectangle(
                    drawPosition.ToPoint() - Constants.OffscreenDistance / new Point(2, 2), 
                    Constants.OffscreenDistance)))
                continue;

            var effects = EntityManager.TryGetComponent<EffectsComponent>(entity, out var effectsComponent) ? 
                effectsComponent.Effects : SpriteEffects.None;
            var tint = EntityManager.TryGetComponent<TintComponent>(entity, out var tintComponent) ? 
                tintComponent.TintColor : Color.White;
            var scale = EntityManager.TryGetComponent<ScaleComponent>(entity, out var scaleComponent) ? 
                scaleComponent.Scale : 1;
            var rotation = EntityManager.TryGetComponent<RotationComponent>(entity, out var rotationComponent) ? 
                rotationComponent.Radians : 0;

            while (EntityManager.GetComponent<RenderOperationsComponent>(entity).RenderOperations.TryDequeue(out var operation))
            { 
                operation.Render(spriteBatch, entity, drawPosition, scale, tint, effects, rotation);
            }
        }
        
        spriteBatch.End();
        
        spriteBatch.GraphicsDevice.SetRenderTarget(null);
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);
        spriteBatch.Draw(_gameScreen, viewportBounds, Color.White);
        spriteBatch.End();
    }
}
