using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SpaceGame {
    
    public abstract class Entity {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Entity Parent { get; set; }
        public List<Entity> Children { get; set; }
        public event EventHandler<EntityUpdateEventArgs> Updated;
        public event EventHandler<EventArgs> Drawn;

        public Entity() {

        }

        public Entity(Entity parent) {
            Parent = parent;
        }

        public virtual void Update(GameTime t) {
            foreach (var child in Children) {
                child.OnUpdated(new EntityUpdateEventArgs(t));
            }
        }

        public virtual void OnUpdated(EntityUpdateEventArgs e) {
            Updated?.Invoke(this, e);
        }

        public virtual void Draw() {
            foreach (var child in Children) {
                child.OnDraw(new EventArgs());
            }
        }

        public virtual void OnDraw(EventArgs e) {
            Drawn?.Invoke(this, e);
        }

        public virtual void Add(Entity entity) {
            entity.Parent?.Remove(entity);
            Children.Add(entity);
            entity.Parent = this;
        }

        public virtual void Remove(Entity entity) {
            Children.Remove(entity);
        }

        public virtual void Destroy() {
            foreach (var child in Children) {
                child.Destroy();
            }
        }
    }

    public class EntityUpdateEventArgs : EventArgs {
        private GameTime time;
        public EntityUpdateEventArgs(GameTime Time) {
            time = Time;
        }
        public GameTime Time => time;
    }
}
