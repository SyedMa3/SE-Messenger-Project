﻿/***************************
* Filename    = ClientStateTest.cs
*
* Author      = Niharika Malvia
*
* Product     = Messenger
* 
* Project     = White-Board
*
* Description = This is for testing ClientState.cs file.
*               It contains all the test cases to check the functioning of Client Side.
***************************/

using System.Windows;
using System.Windows.Media;
using MessengerWhiteboard;
using MessengerWhiteboard.Interfaces;
using MessengerWhiteboard.Models;
using Moq;

namespace MessengerTests.WhiteboardTests
{
    [TestClass]
    public class ClientStateTest
    {
        readonly ClientState _client;
        private readonly Mock<IClientCommunicator> _mockCommunicator;
        private readonly Serializer _serializer;
        readonly Utils _utils;

        public ClientStateTest()
        {
            _client = ClientState.Instance;
            _mockCommunicator = new Mock<IClientCommunicator>();
            _serializer = new Serializer();
            _utils = new Utils();
            _client.SetCommunicator(_mockCommunicator.Object);
        }

        [TestMethod]
        public void AlwaysReturnsSameInstance()
        {
            ClientState client1 = ClientState.Instance;
            ClientState client2 = ClientState.Instance;
            Assert.Equals(client1, client2);
        }

        [TestMethod]
        public void NewUserInfoPassingToCommunicator()
        {
            _mockCommunicator.Setup(a => a.SendToServer(It.IsAny<WBShape>()));
            _client.SetUserId("2");
            _client.InitializeUser();
            WBShape expected = new(null, Operation.NewUser, "2");

            _mockCommunicator.Verify(a => a.SendToServer(It.Is<WBShape>(obj => _utils.CompareBoardServerShapes(obj, expected))), Times.Once);
        }

        [TestMethod]
        public void ClientSendToCommunicator()
        {
            _mockCommunicator.Setup(a => a.SendToServer(It.IsAny<WBShape>()));

            Point start = new(1, 1);
            Point end = new(2, 2);
            ShapeItem boardShape = _utils.CreateShape("EllipseGeometry", start, end, Brushes.Transparent, Brushes.Black, 1, "u0f0");
            List<ShapeItem> newShapes = new()
            {
                boardShape
            };

            List<SerializableShapeItem> newSerializedShapes = _serializer.SerializeShapes(newShapes);
            WBShape expected = new(newSerializedShapes, Operation.Creation);

            _client.OnShapeReceived(boardShape, Operation.Creation);

            _mockCommunicator.Verify(m => m.SendToServer(It.Is<WBShape>(obj => _utils.CompareBoardServerShapes(obj, expected))), Times.Once());
        }
    }
}