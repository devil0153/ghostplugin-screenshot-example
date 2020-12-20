using Ghost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot
{
    public class ScreenshotExtension : IExtension
    {
        private ScreenshotHub screenshotHub;

        public HubBase Hub => screenshotHub;

        public void Initialize(IExtensionConfig config)
        {
            screenshotHub = new ScreenshotHub();
        }

        public void OnConnected()
        {

        }

        public void OnDisconnect()
        {

        }

        public void Dispose()
        {

        }
    }
}
