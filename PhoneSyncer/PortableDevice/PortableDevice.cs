using PortableDeviceApiLib;
using PortableDeviceTypesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSyncer
{
    class PortableDevice
    {
        private bool _isConnected = false;
        private readonly PortableDeviceClass _device;

        public PortableDevice(string deviceId)
        {
            _device = new PortableDeviceClass();
            _deviceId = deviceId;
        }

        private readonly string _deviceId;
        public string DeviceId { get { return _deviceId; } }

        public void Connect()
        {
            if (_isConnected) return;

            var clientInfo = (PortableDeviceApiLib.IPortableDeviceValues)new PortableDeviceValuesClass();
            _device.Open(DeviceId, clientInfo);
            _isConnected = true;
        }

        public void Disconnect()
        {
            if (!_isConnected) return;
            _device.Close();
            _isConnected = false;
        }

        public string FriendlyName
        {
            get
            {
                if (!_isConnected) throw new InvalidOperationException("Not connected to devie.");

                // Retrieve the properties of the device
                _device.Content(out IPortableDeviceContent content);
                content.Properties(out IPortableDeviceProperties properties);

                // Retrieve the values for the properties
                properties.GetValues("DEVICE", null,
                    out PortableDeviceApiLib.IPortableDeviceValues propertyValues);

                // Identify the property to retrieve
                var property = new PortableDeviceApiLib._tagpropertykey();
                property.fmtid = new Guid(0x26D4979A, 0xE643, 0x4626, 0x9E, 0x2B,
                                            0x73, 0x6D, 0xC0, 0xC9, 0x2F, 0xDC);
                property.pid = 12;

                // Retrieve the friendly name
                propertyValues.GetStringValue(ref property, out string propertyValue);

                
                return propertyValue;
            }
        }
    }
}
