using System;

namespace Gameplay.Level
{
    public readonly struct GameObjectPlacement
    {
        public PlacementType Type { get; }
        public int LinkedObjectId { get; }

        public GameObjectPlacement(PlacementType type, int linkedObjectId)
        {
            Type = type;
            LinkedObjectId = linkedObjectId;
        }

        public override bool Equals(object obj)
        {
            return obj is GameObjectPlacement placement && Equals(placement);
        }

        public bool Equals(GameObjectPlacement other)
        {
            return Type == other.Type && LinkedObjectId == other.LinkedObjectId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Type, LinkedObjectId);
        }

        public override string ToString()
        {
            return $"GameObjectPlacement {{Type={Type.ToString()}, LinkedObjectId={LinkedObjectId}}}";
        }
    }

    public enum PlacementType
    {
        GameObjectInstance, PlaceReservation
    }
}