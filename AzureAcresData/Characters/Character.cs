#region File Description
//-----------------------------------------------------------------------------
// Character.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace AzureAcresData
{
    public class Character : ContentObject, ICloneable
    {
        #region Character State

        /// <summary>
        /// The state of a character.
        /// </summary>
        public enum CharacterState
        {
            Idle,

            Walking,

            Acting
        }


        /// <summary>
        /// The state of this character.
        /// </summary>
        private CharacterState state = CharacterState.Idle;

        /// <summary>
        /// The state of this character.
        /// </summary>
        [ContentSerializerIgnore]
        public CharacterState State
        {
            get { return state; }
            set { state = value; }
        }


        /// <summary>
        /// Returns true if the character is dead or dying.
        /// </summary>
        public bool IsDeadOrDying
        {
            get
            {
                return false;
            }
        }

        #endregion


        #region Map Data

        private Point _destinationPosition = new Point(0, 0);
        [ContentSerializerIgnore]
        public Point DestinationPosition
        {
            get { return _destinationPosition; }
            set { _destinationPosition = value; }
        }

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        private Point mapPosition;

        /// <summary>
        /// The position of this object on the map.
        /// </summary>
        [ContentSerializerIgnore]
        public Point MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = value; }
        }


        /// <summary>
        /// The orientation of this object on the map.
        /// </summary>
        private Direction direction;

        /// <summary>
        /// The orientation of this object on the map.
        /// </summary>
        [ContentSerializerIgnore]
        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }


        #endregion


        #region Graphics Data


        /// <summary>
        /// The animating sprite for the map view of this character.
        /// </summary>
        private AnimatingSprite mapSprite;

        /// <summary>
        /// The animating sprite for the map view of this character.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public AnimatingSprite MapSprite
        {
            get { return mapSprite; }
            set { mapSprite = value; }
        }


        /// <summary>
        /// The animating sprite for the map view of this character as it walks.
        /// </summary>
        /// <remarks>
        /// If this object is null, then the animations are taken from MapSprite.
        /// </remarks>
        private AnimatingSprite walkingSprite;

        /// <summary>
        /// The animating sprite for the map view of this character as it walks.
        /// </summary>
        /// <remarks>
        /// If this object is null, then the animations are taken from MapSprite.
        /// </remarks>
        [ContentSerializer(Optional = true)]
        public AnimatingSprite WalkingSprite
        {
            get { return walkingSprite; }
            set { walkingSprite = value; }
        }

        /// <summary>
        /// Reset the animations for this character.
        /// </summary>
        public virtual void ResetAnimation(bool isWalking)
        {
            if (state != CharacterState.Walking && state != CharacterState.Idle) return;
            
            state = isWalking ? CharacterState.Walking : CharacterState.Idle;
            if (mapSprite != null)
                mapSprite.PlayAnimation("Idle", Direction);

            if (walkingSprite != null && state == CharacterState.Walking)
            {
                if (isWalking && walkingSprite["Walk" + Direction.ToString()] != null)
                {
                    walkingSprite.PlayAnimation("Walk", Direction);
                }
                else
                {
                    walkingSprite.PlayAnimation("Idle", Direction);
                }
            }

        }
        #endregion


        #region Standard Animation Data


        /// <summary>
        /// The default idle-animation interval for the animating map sprite.
        /// </summary>
        private int mapIdleAnimationInterval = 1000;

        /// <summary>
        /// The default idle-animation interval for the animating map sprite.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public int MapIdleAnimationInterval
        {
            get { return mapIdleAnimationInterval; }
            set { mapIdleAnimationInterval = value; }
        }


        /// <summary>
        /// Add the standard character idle animations to this character.
        /// </summary>
        protected void AddStandardCharacterIdleAnimations()
        {
            if (mapSprite != null)
            {
                mapSprite.AddAnimation(new Animation("IdleSouth", 1, 1,
                    MapIdleAnimationInterval, true));
                mapSprite.AddAnimation(new Animation("IdleWest", 2, 2,
                    MapIdleAnimationInterval, true));
                mapSprite.AddAnimation(new Animation("IdleNorth", 3, 3,
                    MapIdleAnimationInterval, true));
                mapSprite.AddAnimation(new Animation("IdleEast", 4, 4,
                    MapIdleAnimationInterval, true));

            }
        }

        /// <summary>
        /// The default walk-animation interval for the animating map sprite.
        /// </summary>
        private int mapWalkingAnimationInterval = 150;

        /// <summary>
        /// The default walk-animation interval for the animating map sprite.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public int MapWalkingAnimationInterval
        {
            get { return mapWalkingAnimationInterval; }
            set { mapWalkingAnimationInterval = value; }
        }


        /// <summary>
        /// Add the standard character walk animations to this character.
        /// </summary>
        protected void AddStandardCharacterWalkingAnimations()
        {
            AnimatingSprite sprite = (walkingSprite == null ? mapSprite : walkingSprite);
            if (sprite != null)
            {
                sprite.AddAnimation(new Animation("WalkSouth", 1, 4,
                    MapWalkingAnimationInterval, true));
                sprite.AddAnimation(new Animation("WalkWest", 5, 8,
                    MapWalkingAnimationInterval, true));
                sprite.AddAnimation(new Animation("WalkNorth", 9, 12,
                    MapWalkingAnimationInterval, true));
                sprite.AddAnimation(new Animation("WalkEast", 13, 16,
                    MapWalkingAnimationInterval, true));
            }
        }


        #endregion


        #region Content Type Reader


        /// <summary>
        /// Reads a Character object from the content pipeline.
        /// </summary>
        public class CharacterReader : ContentTypeReader<Character>
        {
            /// <summary>
            /// Reads a Character object from the content pipeline.
            /// </summary>
            protected override Character Read(ContentReader input,
                Character existingInstance)
            {
                Character character = existingInstance;
                if (character == null)
                    character = new Character();

                character.MapIdleAnimationInterval = input.ReadInt32();

                character.MapSprite = input.ReadObject<AnimatingSprite>();
                if (character.MapSprite != null)
                {
                    character.MapSprite.SourceOffset =
                        new Vector2(
                        character.MapSprite.SourceOffset.X - 32,
                        character.MapSprite.SourceOffset.Y - 32);
                }
                character.AddStandardCharacterIdleAnimations();

                character.WalkingSprite = input.ReadObject<AnimatingSprite>();
                if (character.WalkingSprite != null)
                {
                    character.WalkingSprite.SourceOffset =
                        new Vector2(
                        character.WalkingSprite.SourceOffset.X - 32,
                        character.WalkingSprite.SourceOffset.Y - 32);
                }
                character.AddStandardCharacterWalkingAnimations();

                character.MapWalkingAnimationInterval = input.ReadInt32();

                character.ResetAnimation(false);
                return character;
            }
        }


        #endregion

        public object Clone()
        {
            Character character = new Character();
            character.Name = this.Name;
            character.MapPosition = this.MapPosition;
            character.Direction = this.Direction;
            character.MapSprite = this.MapSprite;
            character.WalkingSprite = this.WalkingSprite;
            character.MapIdleAnimationInterval = this.MapIdleAnimationInterval;
            return character;
        }
    }
}
