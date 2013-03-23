using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureAcresData;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace AzureAcresProcessors
{
    [ContentTypeWriter]
    public class MapWriter : ContentTypeWriter<Map>
    {
        protected override void Write(ContentWriter output, Map value)
        {
            output.Write(value.Name);
            output.WriteObject<int[]>(value.MapDimensions);
            output.WriteObject<int[]>(value.TileDimensions);
            output.Write(value.TextureName);
            output.WriteObject<int[]>(value.BaseLayer);
            output.WriteObject<int[]>(value.SecondBaseLayer);
            output.WriteObject<int[]>(value.FringeLayer);
            output.WriteObject<int[]>(value.ObjectLayer);
            output.WriteObject<int[]>(value.CollisionLayer);
            output.WriteObject<string[]>(value.AnimatedTileLayer);
            output.WriteObject<List<Portal>>(value.Portals);
            output.WriteObject<List<ItemContainer>>(value.Containers);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(Map.MapContentReader).AssemblyQualifiedName;
        }
    }
}
