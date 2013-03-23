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
    public class AnimationWriter : ContentTypeWriter<Animation>
    {
        protected override void Write(ContentWriter output, Animation value)
        {
            output.Write(value.Name);
            output.Write(value.StartingFrame);
            output.Write(value.EndingFrame);
            output.Write(value.Interval);
            output.Write(value.IsLoop);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(Animation.AnimationReader).AssemblyQualifiedName;
        }
    }
}
