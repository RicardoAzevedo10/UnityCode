using LiteNetLib.Utils;

namespace LiteNetLibShared
{
    public struct Point : INetSerializable
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(X);
            writer.Put(Y);
        }

        public void Deserialize(NetDataReader reader)
        {
            X = reader.GetInt();
            Y = reader.GetInt();
        }
    }
}