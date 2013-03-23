using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace AzureAcresData
{
    public class Crop : ActionableObject, ICloneable, ICollectableItem
    {
        public delegate void OnFinishedGrowingHandler(object sender);
        public event OnFinishedGrowingHandler OnFinishedGrowing;

        public delegate void OnNoLongerWateredHandler(object sender);
        public event OnNoLongerWateredHandler OnNoLongerWatered;

        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private int _stages;
        public int Stages
        {
            get { return _stages; }
            set { _stages = value; }
        }
        private int _growTime;
        public int GrowTime
        {
            get { return _growTime; }
            set { _growTime = value; }
        }
        private string _textureName;
        public string TextureName
        {
            get { return _textureName; }
            set { _textureName = value; }
        }
        private string _inventoryInitialTextureName;
        public string InventoryInitialTextureName
        {
            get { return _inventoryInitialTextureName; }
            set { _inventoryInitialTextureName = value; }
        }

        private string _inventoryCompleteTextureName;
        public string InventoryCompleteTextureName
        {
            get { return _inventoryCompleteTextureName; }
            set { _inventoryCompleteTextureName = value; }
        }
        private Point _dimensions;
        public Point Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }
        private int _waterTime;
        public int WaterTime
        {
            get { return _waterTime; }
            set { _waterTime = value; }
        }

        private Texture2D _texture;
        [ContentSerializerIgnore]
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        private Texture2D _inventoryInitialTexture;
        [ContentSerializerIgnore]
        public Texture2D InventoryInitialTexture
        {
            get { return _inventoryInitialTexture; }
            set { _inventoryInitialTexture = value; }
        }

        private Texture2D _inventoryCompleteTexture;
        [ContentSerializerIgnore]
        public Texture2D InventoryCompleteTexture
        {
            get { return _inventoryCompleteTexture; }
            set { _inventoryCompleteTexture = value; }
        }

        private Vector2 _coordinates;
        [ContentSerializerIgnore]
        public Vector2 Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        private float _elapsedGrowTime;
        [ContentSerializerIgnore]
        public float ElapsedGrowTime
        {
            get { return _elapsedGrowTime; }
            set { _elapsedGrowTime = value; }
        }

        private bool _isWatered;
        [ContentSerializerIgnore]
        public bool IsWatered
        {
            get { return _isWatered; }
            set { _isWatered = value; ElapsedWaterTime = 0; }
        }

        private bool _isGrown;
        [ContentSerializerIgnore]
        public bool IsGrown
        {
            get { return _isGrown; }
            set { _isGrown = value; }
        }

        private int _currentStage;
        [ContentSerializerIgnore]
        public int CurrentStage
        {
            get { return _currentStage; }
            set { _currentStage = value; }
        }

        private float _stageGrowTime;
        [ContentSerializerIgnore]
        public float StageGrowTime
        {
            get { return _stageGrowTime; }
            set { _stageGrowTime = value; }
        }

        private float _elapsedWaterTime;
        [ContentSerializerIgnore]
        public float ElapsedWaterTime
        {
            get { return _elapsedWaterTime; }
            set { _elapsedWaterTime = value; }
        }

        public void Update(float elapsedMilliseconds)
        {
            if(IsWatered)
                ElapsedWaterTime += elapsedMilliseconds;

            if (IsWatered && !IsGrown)
                ElapsedGrowTime += elapsedMilliseconds;

            if (ElapsedWaterTime >= WaterTime)
            {
                IsWatered = false;
                if (OnNoLongerWatered != null)
                    OnNoLongerWatered(this);
            }

            UpdateStage();
            
            if (ElapsedGrowTime >= GrowTime)
            {
                CurrentStage = Stages;
                IsGrown = true;
                ElapsedGrowTime = GrowTime;
                if (OnFinishedGrowing != null)
                    OnFinishedGrowing(this);
            }
        }
        private void UpdateStage()
        {
            if (!IsGrown)
            {
                if (ElapsedGrowTime > CurrentStage * StageGrowTime)
                    CurrentStage++;
            }
        }

        [ContentSerializerIgnore]
        public Rectangle BoundingBox
        {
            get {
                Rectangle box = new Rectangle((int)Coordinates.X, (int)Coordinates.Y, 32, 32);
                return box;
            }
        }

        public class CropReader : ContentTypeReader<Crop>
        {
            protected override Crop Read(ContentReader input, Crop existingInstance)
            {
                Crop existing = existingInstance;
                if (existing == null)
                    existing = new Crop();
                existing.Name = input.ReadString();
                existing.Type = input.ReadString();
                existing.Stages = input.ReadInt32();
                existing.GrowTime = input.ReadInt32();
                existing.TextureName = input.ReadString();
                existing.Texture = input.ContentManager.Load<Texture2D>(existing.TextureName);
                existing.InventoryInitialTextureName = input.ReadString();
                existing.InventoryInitialTexture = input.ContentManager.Load<Texture2D>(existing.InventoryInitialTextureName);
                existing.InventoryCompleteTextureName = input.ReadString();
                existing.InventoryCompleteTexture = input.ContentManager.Load<Texture2D>(existing.InventoryCompleteTextureName);
                existing.Dimensions = input.ReadObject<Point>();
                existing.WaterTime = input.ReadInt32();

                existing.ElapsedGrowTime = 0;
                existing.IsGrown = false;
                existing.IsWatered = false;
                existing.CurrentStage = 1;
                existing.StageGrowTime = existing.GrowTime / (existing.Stages - 1);
                return existing;
            }
        }

        public object Clone()
        {
            Crop crop = new Crop();
            crop.Name = this.Name;
            crop.Type = this.Type;
            crop.Stages = this.Stages;
            crop.GrowTime = this.GrowTime;
            crop.TextureName = this.TextureName;
            crop.Texture = this.Texture;
            crop.InventoryInitialTextureName = this.InventoryInitialTextureName;
            crop._inventoryInitialTexture = this.InventoryInitialTexture;
            crop.InventoryCompleteTextureName = this.InventoryCompleteTextureName;
            crop.InventoryCompleteTexture = this.InventoryCompleteTexture;
            crop.Dimensions = this.Dimensions;
            crop.ElapsedGrowTime = this.ElapsedGrowTime;
            crop.IsGrown = this.IsGrown;
            crop.IsWatered = this.IsWatered;
            crop.CurrentStage = this.CurrentStage;
            crop.StageGrowTime = this.StageGrowTime;
            crop.WaterTime = this.WaterTime;
            return crop;
        }

        public Texture2D GetItemTexture()
        {
            return (IsGrown) ? InventoryCompleteTexture : InventoryInitialTexture;
        }

        public string GetItemName()
        {
            return this.Name;
        }
    }
}
