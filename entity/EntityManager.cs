using System;
using System.Collections.Generic;
using System.Linq;
using solo_slasher.component;

namespace solo_slasher;

public static class EntityManager {
    private static readonly Dictionary<long, Entity> Entities = new();
    private static readonly Dictionary<Type, Dictionary<long, IComponent>> Components = new();
    private static long _nextEntityId;

    public static Entity CreateEntity() {
        var id = _nextEntityId++;
        var entity = new Entity(id);
        Entities[id] = entity;
        Console.WriteLine($"[E{id}] Created");
        return entity;
    }

    public static void DestroyEntity(Entity entity)
    {
        if (!Entities.Remove(entity.Id)) return;
        
        foreach (var componentList in Components.Values) {
            componentList.Remove(entity.Id);
        }
        
        Console.WriteLine($"[E{entity.Id}] Destroyed");
    }

    public static void AddComponent<T>(Entity entity, T component) where T : IComponent {
        if (!Components.ContainsKey(typeof(T))) {
            Components[typeof(T)] = new Dictionary<long, IComponent>();
        }
        if (!Components[typeof(T)].ContainsKey(entity.Id))
            Console.WriteLine($"[E{entity.Id}] Adding component {typeof(T).Name}");
        Components[typeof(T)][entity.Id] = component;
    }

    public static bool HasComponent<T>(Entity entity) where T : IComponent {
        return Components.ContainsKey(typeof(T)) && Components[typeof(T)].ContainsKey(entity.Id);
    }

    public static T GetComponent<T>(Entity entity) where T : IComponent {
        return (T)Components[typeof(T)][entity.Id];
    }

    public static bool TryGetComponent<T>(Entity entity, out T component) where T : IComponent
    {
        if (!HasComponent<T>(entity))
        {
            component = default;
            return false;
        }
        component = GetComponent<T>(entity);
        return true;

    }

    public static void RemoveComponent<T>(Entity entity) where T : IComponent
    {
        if (!Components.ContainsKey(typeof(T))) return;
        if (Components[typeof(T)].ContainsKey(entity.Id))
            Console.WriteLine($"[E{entity.Id}] Removing component {typeof(T).Name}");
        Components[typeof(T)].Remove(entity.Id);
    }

    public static List<Entity> GetEntitiesWith<T>() where T : IComponent
    {
        return !Components.ContainsKey(typeof(T)) ? [] : 
            Components[typeof(T)].Keys
                .Select(id => Entities[id])
                .ToList();
    }
    
    public static List<Entity> GetEntitiesWith(params Type[] requiredComponents) {
        if (requiredComponents.Length == 0) return [];

        var validEntityIds = new HashSet<long>(Entities.Keys);

        foreach (var componentType in requiredComponents) {
            if (!Components.TryGetValue(componentType, out var componentEntities)) return [];
            validEntityIds.IntersectWith(componentEntities.Keys);
        }

        return validEntityIds.Select(id => Entities[id]).ToList();
    }
    
    public static List<Entity> GetEntitiesWithAny(params Type[] anyComponents) {
        var validEntityIds = new HashSet<long>(Entities.Keys);

        foreach (var componentType in anyComponents) {
            if (Components.TryGetValue(componentType, out var componentEntities)) 
                validEntityIds.UnionWith(componentEntities.Keys);
        }
        
        // if (!_components.TryGetValue(typeof(T), out var requiredComponentEntities)) return [];
        // validEntityIds.IntersectWith(requiredComponentEntities.Keys);

        return validEntityIds.Select(id => Entities[id]).ToList();
    }

    public static bool TryGetFirstEntityWith<T>(out Entity entity) where T : IComponent
    {
        entity = null;
        if (!Components.ContainsKey(typeof(T)) || Components[typeof(T)].Count <= 0) return false;
        entity = Entities[Components[typeof(T)].Keys.First()];
        return true;
    }

}
