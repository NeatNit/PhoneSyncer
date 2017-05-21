using PortableDeviceApiLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSyncer
{
    // following the guide at https://cgeers.wordpress.com/2011/05/22/enumerating-windows-portable-devices/
    // full list for later: https://cgeers.wordpress.com/category/programming/wpd/
    class PortableDeviceCollection : Collection<PortableDevice>
    {
        private readonly PortableDeviceManagerClass _deviceManager;

        public PortableDeviceCollection()
        {
            this._deviceManager = new PortableDeviceManagerClass();
        }

        public void Refresh()
        {
            _deviceManager.RefreshDeviceList();
            Clear();

            // Determine how many WPD devices are connected
            var deviceIds = new string[1];
            uint count = 1;
            _deviceManager.GetDevices(ref deviceIds[0], ref count);

            if (count == 0) return; // no devices connected

            // Retrieve the device ID for each connected device
            deviceIds = new string[count];
            _deviceManager.GetDevices(ref deviceIds[0], ref count);

            // Add devices to our collection
            for (int i = 0; i < deviceIds.Length; i++)
            {
                Add(new PortableDevice(deviceIds[i]));
            }
        }
    }
}
