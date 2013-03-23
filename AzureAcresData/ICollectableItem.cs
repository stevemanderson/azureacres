using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AzureAcresData
{
    public interface ICollectableItem
    {
        Texture2D GetItemTexture();
        string GetItemName();
    }
}
