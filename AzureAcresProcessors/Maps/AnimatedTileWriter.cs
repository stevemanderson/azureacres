using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureAcresData;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework;

namespace AzureAcresProcessors
{
    [ContentTypeWriter]
    public class AnimatedTileWriter : ContentTypeWriter<AnimatedTile>
    {
        protected override void Write(ContentWriter output, AnimatedTile value)
        {
            output.Write(value.Name);
            output.WriteObject<AnimatingSprite>(value.Sprite);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(AnimatedTile.AnimatedTileReader).AssemblyQualifiedName;
        }
    }
}
