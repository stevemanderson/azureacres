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
    public class CropWriter : ContentTypeWriter<Crop>
    {
        protected override void Write(ContentWriter output, Crop value)
        {
            output.Write(value.Name);
            output.Write(value.Type);
            output.Write(value.Stages);
            output.Write(value.GrowTime);
            output.Write(value.TextureName);
            output.Write(value.InventoryInitialTextureName);
            output.Write(value.InventoryCompleteTextureName);
            output.WriteObject<Point>(value.Dimensions);
            output.Write(value.WaterTime);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(Crop.CropReader).AssemblyQualifiedName;
        }
    }
}
