using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Threading;

namespace AngularDashboardBackend.Hubs
{
    public class DashboardConnection : PersistentConnection
    {
        private static int _connections = 0;
        private static IPersistentConnectionContext _context;

        protected override Task OnConnected(IRequest request, string connectionId)
        {
            if (Interlocked.Increment(ref _connections) == 1)
                Task.Factory.StartNew(StartBroadcasting);

            return base.OnConnected(request, connectionId);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Decrement(ref _connections);

            return base.OnDisconnected(request, connectionId, stopCalled);
        }

        private static void StartBroadcasting()
        {
            if (_context == null)
                _context = GlobalHost.ConnectionManager.GetConnectionContext<DashboardConnection>();

            var data = InitData(100);

            while (_connections > 0)
            {
                Thread.Sleep(1000);

                try
                {
                    data = ProgressData(data);
                    _context.Connection.Broadcast(data);
                }
                catch
                {}
            }
        }

        private static IEnumerable<int> ProgressData(IEnumerable<int> data)
        {
            var result = data.ToList();
            result.Add(result.First());
            return result.Skip(1);
        }

        private static IEnumerable<int> InitData(int length)
        {
            var rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                yield return rnd.Next(100);
            }
        }
    }
}