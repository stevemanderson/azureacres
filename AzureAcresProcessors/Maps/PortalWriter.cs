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
    public class PortalWriter : ContentTypeWriter<Portal>
    {
        protected override void Write(ContentWriter output, Portal value)
        {
            output.Write(value.Name);
            output.Write(value.ToMapName);
            output.Write(value.ToPortalName);
            output.WriteObject<Point>(value.Coordinates);
            output.Write(value.Direction);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(Portal.PortalReader).AssemblyQualifiedName;
        }
    }
}
