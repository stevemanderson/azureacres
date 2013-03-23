using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace AzureAcresData
{
    public abstract class ActionableObject : ContentObject
    {
        public virtual void Act(ContentObject actingObject, Character actingCharacter, Vector2 location) {}
    }
}
