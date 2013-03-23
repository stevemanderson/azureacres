#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using AzureAcresData;
#endregion

namespace AzureAcresProcessors
{
    [ContentTypeWriter]
    public class AnimatingSpriteWriter : ContentTypeWriter<AnimatingSprite>
    {
        protected override void Write(ContentWriter output, AnimatingSprite value)
        {
            output.Write(value.TextureName);
            output.WriteObject(value.FrameDimensions);
            output.Write(value.FramesPerRow);
            output.WriteObject(value.SourceOffset);
            output.WriteObject(value.Animations);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(AnimatingSprite.AnimatingSpriteReader).AssemblyQualifiedName;
        }
    }
}
