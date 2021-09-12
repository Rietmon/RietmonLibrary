using System;
using Rietmon.Extensions;
using Rietmon.Serialization;

namespace Rietmon.Other
{
    [Serializable]
    public struct Id : ISerializable
    {
        private Identification identification;

        private Id(Identification identification)
        {
            this.identification = identification;
        }
        
        public void Serialize(SerializationStream stream)
        {
            if (identification == null)
            {
                stream.Write<byte>(0);
                return;
            }
            
            stream.Write(identification.Size);
            stream.Write(identification.Id);
        }

        public void Deserialize(SerializationStream stream)
        {
            var identificationSize = stream.Read<byte>();

            switch (identificationSize)
            {
                case 1: identification = new Identification8(stream.Read<byte>()); break;
                case 2: identification = new Identification16(stream.Read<short>()); break;
                case 4: identification = new Identification32(stream.Read<int>()); break;
                case 8: identification = new Identification64(stream.Read<long>()); break;
                case 16: identification = new Identification128(stream.Read<decimal>()); break;
            }
        }

        public static bool operator ==(Id left, Id right) => 
            left.identification == right.identification;

        public static bool operator !=(Id first, Id second) => 
            !(first == second);

        public static Id Create8() => Create8(RandomUtilities.RandomByte);
        public static Id Create8(byte id) => new Id(new Identification8(id));
        
        public static Id Create16() => Create16(RandomUtilities.RandomShort);
        public static Id Create16(short id) => new Id(new Identification16(id));
        
        public static Id Create32() => Create32(RandomUtilities.RandomInt);
        public static Id Create32(int id) => new Id(new Identification32(id));
        
        public static Id Create64() => Create64(RandomUtilities.RandomLong);
        public static Id Create64(long id) => new Id(new Identification64(id));
        
        public static Id Create128() => Create128(RandomUtilities.RandomDecimal);
        public static Id Create128(decimal id) => new Id(new Identification128(id));
    }
}