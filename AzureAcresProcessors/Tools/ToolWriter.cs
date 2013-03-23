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
    public class ToolWriter : ContentTypeWriter<Tool>
    {
        protected override void Write(ContentWriter output, Tool value)
        {
            output.Write(value.Name);
            output.Write(value.Type);
            output.Write(value.InventoryTextureName);
            output.WriteObject(value.Sprite);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(Tool.ToolReader).AssemblyQualifiedName;
        }
    }
}
