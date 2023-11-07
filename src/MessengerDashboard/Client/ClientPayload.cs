﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerDashboard.Client
{
    public class ClientPayload
    {
        public Operation Operation { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhotoURL { get; set; }

        public string IpAddress { get; set; } = string.Empty;

        public int Port { get; set; } = -1;
        public ClientPayload(Operation eventName, string clientName, string ipAddress, int port, int clientID = -1, string clientEmail = null, string clientPhotoUrl = null)
        {
            Operation = eventName;
            UserName = clientName;
            UserID = clientID;
            UserEmail = clientEmail;
            UserPhotoURL = clientPhotoUrl;
            IpAddress = ipAddress;
            Port = port;
        }

        public ClientPayload()
        {

        }
    }
}