using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureAcresData;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace AzureAcresProcessors
{
    [ContentTypeWriter]
    public class ItemContainerWriter : ContentTypeWriter<ItemContainer>
    {
        protected override void Write(ContentWriter output, ItemContainer value)
        {
            output.Write(value.Name);
            output.Write(value.ContentsWidth);
            output.Write(value.ContentsHeight);
            output.Write(value.Dimensions);
            output.Write(value.Position);
            output.Write(value.BackgroundTextureName);
            output.Write(value.SelectedTextureName);
            output.WriteObject<ICollectableItem[]>(value.Items);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(ItemContainer.ItemContainerContentReader).AssemblyQualifiedName;
        }
    }
}
