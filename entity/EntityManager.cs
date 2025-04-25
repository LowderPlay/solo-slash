using System;
using System.Collections.Generic;
using System.Linq;
using solo_slasher.component;

namespace solo_slasher;

public class EntityManager {
    private readonly Dictionary<long, Entity> _entities = new();
    private readonly Dictionary<Type, Dictionary<long, IComponent>> _components = new();
    private long _nextEntityId;

    public Entity CreateEntity() {
        var id = _nextEntityId++;
        var entity = new Entity(id);
        _entities[id] = entity;
        Console.WriteLine($"[E{id}] Created");
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        if (!_entities.Remove(entity.Id)) return;
        
        foreach (var componentList in _components.Values) {
            componentList.Remove(entity.Id);
        }
        
        Console.WriteLine($"[E{entity.Id}] Destroyed");
    }

    public void AddComponent<T>(Entity entity, T component) where T : IComponent {
        if (!_components.ContainsKey(typeof(T))) {
            _components[typeof(T)] = new Dictionary<long, IComponent>();
        }
        if (!_components[typeof(T)].ContainsKey(entity.Id))
            Console.WriteLine($"[E{entity.Id}] Adding component {typeof(T).Name}");
        _components[typeof(T)][entity.Id] = component;
    }

    public bool HasComponent<T>(Entity entity) where T : IComponent {
        return _components.ContainsKey(typeof(T)) && _components[typeof(T)].ContainsKey(entity.Id);
    }

    public T GetComponent<T>(Entity entity) where T : IComponent {
        return (T)_components[typeof(T)][entity.Id];
    }

    public bool TryGetComponent<T>(Entity entity, out T component) where T : IComponent
    {
        if (!HasComponent<T>(entity))
        {
            component = default;
            return false;
        }
        component = GetComponent<T>(entity);
        return true;

    }

    public void RemoveComponent<T>(Entity entity) where T : IComponent
    {
        if (!_components.ContainsKey(typeof(T))) return;
        if (_components[typeof(T)].ContainsKey(entity.Id))
            Console.WriteLine($"[E{entity.Id}] Removing component {typeof(T).Name}");
        _components[typeof(T)].Remove(entity.Id);
    }

    public List<Entity> GetEntitiesWith<T>() where T : IComponent
    {
        return !_components.ContainsKey(typeof(T)) ? [] : 
            _components[typeof(T)].Keys
                .Select(id => _entities[id])
                .ToList();
    }
    
    public List<Entity> GetEntitiesWith(params Type[] requiredComponents) {
        if (requiredComponents.Length == 0) return [];

        var validEntityIds = new HashSet<long>(_entities.Keys);

        foreach (var componentType in requiredComponents) {
            if (!_components.TryGetValue(componentType, out var componentEntities)) return [];
            validEntityIds.IntersectWith(componentEntities.Keys);
        }

        return validEntityIds.Select(id => _entities[id]).ToList();
    }
    
    public List<Entity> GetEntitiesWithAny(params Type[] anyComponents) {
        var validEntityIds = new HashSet<long>(_entities.Keys);

        foreach (var componentType in anyComponents) {
            if (_components.TryGetValue(componentType, out var componentEntities)) 
                validEntityIds.UnionWith(componentEntities.Keys);
        }
        
        // if (!_components.TryGetValue(typeof(T), out var requiredComponentEntities)) return [];
        // validEntityIds.IntersectWith(requiredComponentEntities.Keys);

        return validEntityIds.Select(id => _entities[id]).ToList();
    }

    public bool TryGetFirstEntityWith<T>(out Entity entity) where T : IComponent
    {
        entity = null;
        if (!_components.ContainsKey(typeof(T)) || _components[typeof(T)].Count <= 0) return false;
        entity = _entities[_components[typeof(T)].Keys.First()];
        return true;
    }

}
