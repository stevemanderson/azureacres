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
    public class CharacterWriter : ContentTypeWriter<Character>
    {
        protected override void Write(ContentWriter output, Character value)
        {
            output.Write(value.MapIdleAnimationInterval);
            output.WriteObject(value.MapSprite);
            output.WriteObject(value.WalkingSprite);
            output.Write(value.MapWalkingAnimationInterval);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(Character.CharacterReader).AssemblyQualifiedName;
        }
    }
}
