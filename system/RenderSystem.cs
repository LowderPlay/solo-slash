using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.system;

public class RenderSystem(EntityManager entityManager, SpriteBatch spriteBatch)
{
    public void Render(Rectangle viewportBounds, GameTime gameTime)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        var cameraPosition = Vector2.Zero;
        var camera = entityManager.GetEntitiesWith(typeof(PositionComponent), typeof(CameraOriginComponent));
        if (camera.Count > 0 && entityManager.TryGetComponent<PositionComponent>(camera.First(), out var cameraPositionComponent))
        {
            cameraPosition = cameraPositionComponent.Position;
        }
        
        var screenOrigin = new Vector2(viewportBounds.Width / 2f, viewportBounds.Height / 2f);

        foreach (var entity in entityManager.GetEntitiesWithAny(typeof(TextureComponent), typeof(TextComponent))
                     .OrderBy(e => entityManager.TryGetComponent<ZOrderComponent>(e, out var z) ? z.ZOrder : 0)
                     .ThenBy(e => entityManager.TryGetComponent<PositionComponent>(e, out var pos) ? 
                         pos.Position.Y : 0)
                     .ThenBy(e => entityManager.TryGetComponent<ScreenPositionComponent>(e, out var pos) ? 
                         pos.Position.Y : 0))
        {
            if (entityManager.HasComponent<HiddenComponent>(entity)) continue;
            var drawPosition = entityManager.TryGetComponent<ScreenPositionComponent>(entity, out var pos) ? 
                pos.Position : screenOrigin;

            if (!entityManager.HasComponent<CameraOriginComponent>(entity) && 
                entityManager.TryGetComponent<PositionComponent>(entity, out var positionComponent))
            {
                drawPosition += positionComponent.Position - cameraPosition;
            }

            var effects = SpriteEffects.None;
            if (entityManager.TryGetComponent<MovementFlipComponent>(entity, out var flip) && flip.Flip)
            {
                effects |= SpriteEffects.FlipHorizontally;
            }
            
            var tint = entityManager.TryGetComponent<TintComponent>(entity, out var tintComponent) 
                ? tintComponent.TintColor : Color.White;
            var scale = entityManager.TryGetComponent<ScaleComponent>(entity, out var scaleComponent) ? 
                scaleComponent.Scale : 1;
            
            if (entityManager.TryGetComponent<TextureComponent>(entity, out var texture))
            {
                var spriteSize = (entityManager.TryGetComponent<SizeComponent>(entity, out var size) ? 
                    size.Size : new Vector2(texture.Texture.Width, texture.Texture.Height)) * scale;

                var alignedLocation = drawPosition - spriteSize * texture.Alignment;
                spriteBatch.Draw(texture.Texture, new Rectangle(alignedLocation.ToPoint(), spriteSize.ToPoint()), 
                    null, tint, 0f, Vector2.Zero, effects, 1f);
            } 
            
            if (entityManager.TryGetComponent<TextComponent>(entity, out var text))
            {
                var textSize = text.Font.MeasureString(text.Text) * scale;
                
                var alignedLocation = drawPosition - textSize * text.Alignment;
                spriteBatch.DrawString(text.Font, text.Text, alignedLocation, tint, 
                    0f, Vector2.Zero, scale, effects, 1f);
            }
        }
        
        spriteBatch.End();
    }
}