﻿using UnityEngine;
using Unity.Networking.Transport;

public struct TransportEvent
{
    public enum Type
    {
        Data,
        Connect,
        Disconnect
    }
    public Type type;
    public int connectionId;
    public byte[] data;
    public int dataSize;
}

public interface INetworkTransport
{
    int Connect(string ip, ushort port);
    void Disconnect(int connectionId);

    bool NextEvent(ref TransportEvent e);
    void SendData(int connectionId, byte[] data, int sendSize);
    void Update();

    void Shutdown();
}

public interface INetworkCallbacks
{
    void OnConnect(int clientId);
    void OnDisconnect(int clientId);
    //void OnEvent(int clientId, NetworkEvent info);

    void OnConnectionAck(int clientId);
}

public static class NetworkConfig
{
    public const int defaultServerPort = 7050;
    public const int maxFragments = 16;
    public const int packageFragmentSize = NetworkParameterConstants.MTU - 128;  // 128 is just a random safety distance to MTU
    public const int maxPackageSize = maxFragments * packageFragmentSize;
    public const int disconnectTimeout = 30000;
}

public enum NetworkCommandType
{
    PlayerConnected = 1 << 0,

    ConnectionAck = 1 << 1,

    PlayerCommand = 1 << 2
}

public struct NetworkCommand
{
    public int playerId;

    public string testMessage;

    public NetworkCommandType type;
}
