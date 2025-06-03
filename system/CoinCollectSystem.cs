using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.config;

namespace solo_slasher.system;

public class CoinCollectSystem
{
    public void Update()
    {
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player)) return;
        var playerPosition = EntityManager.GetComponent<PositionComponent>(player);
        foreach (var coin in EntityManager.GetEntitiesWith<CollectableCoinComponent>())
        {
            var coinPosition = EntityManager.GetComponent<PositionComponent>(coin);
            if((playerPosition.Position - coinPosition.Position).Length() > 50) continue;
            EntityManager.DestroyEntity(coin);
            Assets.CoinEffect.Play(ConfigManager.Config.SoundVolume, 0, 0);
            ConfigManager.Config.CoinBalance++;
        }
    }
}
