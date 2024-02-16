using System;
using Unity.Collections;
using Unity.Netcode;

public struct UserLobbyData : INetworkSerializable, IEquatable<UserLobbyData>
{
    public ulong Id;
    public FixedString128Bytes Name;
    public bool Ready;

    public bool Equals(UserLobbyData other)
    {
        return Id == other.Id;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Id);
        serializer.SerializeValue(ref Name);
        serializer.SerializeValue(ref Ready);
    }
}
