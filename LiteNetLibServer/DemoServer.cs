using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using LiteNetLibShared;
using Serilog;

namespace LiteNetLibServer
{
    public class DemoServer : INetEventListener
    {
        public NetManager Server { get; set; }
        private NetPacketProcessor processor;

        public DemoServer()
        {
            processor = new NetPacketProcessor();
            processor.RegisterNestedType<Point>();
            processor.SubscribeReusable<SimplePacket, NetPeer>(SimplePacketHandler);
            processor.SubscribeReusable<CommandPacket, NetPeer>(CommandPacketHandler);
        }

        private void CommandPacketHandler(CommandPacket payload, NetPeer peer)
        {
            Log.Information("Received Packet {Type}", payload);
        }

        private void SimplePacketHandler(SimplePacket payload, NetPeer peer)
        {
            Log.Information("Received Packet {Type}", payload);
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Log.Information("Peer connected: {Peer}", peer.EndPoint);
            List<NetPeer> peers = Server.ConnectedPeerList;
            
            foreach  (NetPeer netPeer in peers)
            {
                Log.Information("ConnectedPeersList: id={Id}, ep={Peer}", netPeer.Id, netPeer.EndPoint);
            }
            
            NetDataWriter writer = new NetDataWriter();
            writer.Put("First Message!");
            peer.Send(writer, DeliveryMethod.Unreliable);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Log.Information("Client {Id} disconnected: {Reason}", peer.Id, disconnectInfo.Reason);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            Log.Information("Error: {Error}", socketError);
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            processor.ReadAllPackets(reader, peer);
            reader.Recycle();
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            string message = reader.GetString(100);
            Log.Information("Received unconnected message {MessageType} from {RemoteEndPoint}: {Data}", messageType, remoteEndPoint, message);
            NetDataWriter writer = new NetDataWriter();

            if (messageType == UnconnectedMessageType.DiscoveryRequest)
            {
                writer.Put("ACK");
            }
            else
            {
                writer.Put("NACK");
            }
            
            Server.SendDiscoveryResponse(writer, remoteEndPoint);
            reader.Recycle();
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            if (request.AcceptIfKey("random_generated_token") == null)
            {
                Log.Information("Connection denied!");
            }
        }
    }
}