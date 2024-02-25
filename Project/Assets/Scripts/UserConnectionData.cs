using System;
using Unity.Collections;
using Unity.Netcode;

public struct UserConnectionData : INetworkSerializable, IEquatable<UserConnectionData>
{
    public ulong Id;
    public FixedString128Bytes Name;

    public bool Equals(UserConnectionData other)
    {
        return Id == other.Id;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Id);
        serializer.SerializeValue(ref Name);
    }
}
