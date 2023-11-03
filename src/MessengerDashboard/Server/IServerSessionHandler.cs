﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerNetworking.NotificationHandler;

namespace MessengerDashboard.Server
{
    /// <summary>
    /// Provides an interface for a server session handler.
    /// </summary>
    internal interface IServerSessionHandler : INotificationHandler
    {
        /// <summary>
        /// Fires when session is updated.
        /// </summary>
        event EventHandler<SessionUpdatedEventArgs> SessionUpdated;
    }
}