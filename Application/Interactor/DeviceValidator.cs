﻿using Domain.Ports.Incoming;
using System.Text;
using System.Security.Cryptography;

namespace Application.Interactor
{
    public class DeviceValidator : IDeviceValidator
    {
        public bool ValidateEncryptedDevice(string deviceId, string sendId, string encryptedDevice)
        {
                byte[] entradaBytes = Encoding.UTF8.GetBytes(deviceId + sendId);
                byte[] salidaBytes = SHA256.HashData(entradaBytes);
                string hash = BitConverter.ToString(salidaBytes).Replace("-", "").ToLower();

                return string.Equals(hash, encryptedDevice, StringComparison.Ordinal);
        }
    }
}
