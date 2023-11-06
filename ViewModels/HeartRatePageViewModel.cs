using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

namespace VRCT.ViewModels;

public partial class HeartRatePageViewModel : BaseViewModel
{
    public BluetoothLEService BluetoothLEService { get; private set; }

    public IAsyncRelayCommand ConnectToDeviceCandidateAsyncCommand { get; }
    public IAsyncRelayCommand DisconnectFromDeviceAsyncCommand { get; }

    public IService HeartRateService { get; private set; }
    public ICharacteristic HeartRateMeasurementCharacteristic { get; private set; }
    public HeartRatePageViewModel(BluetoothLEService bluetoothLEService)
    {
        Title = $"Heart rate";

        BluetoothLEService = bluetoothLEService;

        ConnectToDeviceCandidateAsyncCommand = new AsyncRelayCommand(ConnectToDeviceCandidateAsync);

        DisconnectFromDeviceAsyncCommand = new AsyncRelayCommand(DisconnectFromDeviceAsync);
    }

    [ObservableProperty]
    ushort heartRateValue;

    [ObservableProperty]
    DateTimeOffset timestamp;

    private async Task ConnectToDeviceCandidateAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (BluetoothLEService.NewDeviceCandidateFromDevicePage.Id.Equals(Guid.Empty))
        {
            #region read device id from storage
            var device_name = await SecureStorage.Default.GetAsync("device_name");
            var device_id = await SecureStorage.Default.GetAsync("device_id");
            if (!string.IsNullOrEmpty(device_id))
            {
                BluetoothLEService.NewDeviceCandidateFromDevicePage.Name = device_name;
                BluetoothLEService.NewDeviceCandidateFromDevicePage.Id = Guid.Parse(device_id);
            }
            #endregion read device id from storage
            else
            {
                await BluetoothLEService.ShowToastAsync($"Select a Bluetooth LE device first. Try again.");
                return;
            }
        }

        if (!BluetoothLEService.BluetoothLE.IsOn)
        {
            await Shell.Current.DisplayAlert($"Bluetooth is not on", $"Please turn Bluetooth on and try again.", "OK");
            return;
        }

        if (BluetoothLEService.Adapter.IsScanning)
        {
            await BluetoothLEService.ShowToastAsync($"Bluetooth adapter is scanning. Try again.");
            return;
        }

        try
        {
            IsBusy = true;

            if (BluetoothLEService.Device != null)
            {
                if (BluetoothLEService.Device.State == DeviceState.Connected)
                {
                    if (BluetoothLEService.Device.Id.Equals(BluetoothLEService.NewDeviceCandidateFromDevicePage.Id))
                    {
                        await BluetoothLEService.ShowToastAsync($"WARNING! Heart rate threshold reached.");
                        return;
                    }

                    if (BluetoothLEService.NewDeviceCandidateFromDevicePage != null)
                    {
                        #region another device
                        if (!BluetoothLEService.Device.Id.Equals(BluetoothLEService.NewDeviceCandidateFromDevicePage.Id))
                        {
                            Title = $"{BluetoothLEService.NewDeviceCandidateFromDevicePage.Name}";
                            await DisconnectFromDeviceAsync();
                            await BluetoothLEService.ShowToastAsync($"{BluetoothLEService.Device.Name} has been disconnected.");
                        }
                        #endregion another device
                    }
                }
            }

            BluetoothLEService.Device = await BluetoothLEService.Adapter.ConnectToKnownDeviceAsync(BluetoothLEService.NewDeviceCandidateFromDevicePage.Id);

            if (BluetoothLEService.Device.State == DeviceState.Connected)
            {
                HeartRateService = await BluetoothLEService.Device.GetServiceAsync(HeartRateUuids.HeartRateServiceUuid);
                if (HeartRateService != null)
                {
                    HeartRateMeasurementCharacteristic = await HeartRateService.GetCharacteristicAsync(HeartRateUuids.HeartRateMeasurementCharacteristicUuid);
                    if (HeartRateMeasurementCharacteristic != null)
                    {
                        if (HeartRateMeasurementCharacteristic.CanUpdate)
                        {
                            Title = $"{BluetoothLEService.Device.Name}";

                            #region save device id to storage
                            await SecureStorage.Default.SetAsync("device_name", $"{BluetoothLEService.Device.Name}");
                            await SecureStorage.Default.SetAsync("device_id", $"{BluetoothLEService.Device.Id}");
                            #endregion save device id to storage

                            HeartRateMeasurementCharacteristic.ValueUpdated += HeartRateMeasurementCharacteristic_ValueUpdatedAsync;
                            await HeartRateMeasurementCharacteristic.StartUpdatesAsync();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to connect to {BluetoothLEService.NewDeviceCandidateFromDevicePage.Name} {BluetoothLEService.NewDeviceCandidateFromDevicePage.Id}: {ex.Message}.");
            await Shell.Current.DisplayAlert($"{BluetoothLEService.NewDeviceCandidateFromDevicePage.Name}", $"Unable to connect to {BluetoothLEService.NewDeviceCandidateFromDevicePage.Name}.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async void HeartRateMeasurementCharacteristic_ValueUpdatedAsync(object sender, CharacteristicUpdatedEventArgs e)
    {
        var bytes = e.Characteristic.Value;
        const byte heartRateValueFormat = 0x01;

        byte flags = bytes[0];
        bool isHeartRateValueSizeLong = (flags & heartRateValueFormat) != 0;
        Dataset.HeartRateDevice = isHeartRateValueSizeLong ? BitConverter.ToUInt16(bytes, 1) : bytes[1];
        HeartRateValue = isHeartRateValueSizeLong ? BitConverter.ToUInt16(bytes, 1) : bytes[1];
		Timestamp = DateTimeOffset.Now.LocalDateTime;
		if (Dataset.HeartRateDevice >= (150 - Dataset.Age) * 0.8)
		{
			IAsyncRelayCommand HRTreached = new AsyncRelayCommand(HRreached);
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

			string text = $"WARNING! Heart rate threshold reached.";
			ToastDuration duration = ToastDuration.Short;
			double fontSize = 14;

			var toast = Toast.Make(text, duration, fontSize);


			await MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.DisplayAlert($"WARNING", $"Heart rate threshold reached.", "OK"));
		}
    }

    private async Task HRreached()
    {
        await BluetoothLEService.ShowToastAsync($"WARNING! Heart rate threshold reached.");
        return;
    }


	private async Task DisconnectFromDeviceAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (BluetoothLEService.Device == null)
        {
            await BluetoothLEService.ShowToastAsync($"Nothing to do.");
            return;
        }

        if (!BluetoothLEService.BluetoothLE.IsOn)
        {
            await Shell.Current.DisplayAlert($"Bluetooth is not on", $"Please turn Bluetooth on and try again.", "OK");
            return;
        }

        if (BluetoothLEService.Adapter.IsScanning)
        {
            await BluetoothLEService.ShowToastAsync($"Bluetooth adapter is scanning. Try again.");
            return;
        }

        if (BluetoothLEService.Device.State == DeviceState.Disconnected)
        {
            await BluetoothLEService.ShowToastAsync($"{BluetoothLEService.Device.Name} is already disconnected.");
            return;
        }

        try
        {
            IsBusy = true;

            await HeartRateMeasurementCharacteristic.StopUpdatesAsync();

            await BluetoothLEService.Adapter.DisconnectDeviceAsync(BluetoothLEService.Device);

            HeartRateMeasurementCharacteristic.ValueUpdated -= HeartRateMeasurementCharacteristic_ValueUpdatedAsync;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to disconnect from {BluetoothLEService.Device.Name} {BluetoothLEService.Device.Id}: {ex.Message}.");
            await Shell.Current.DisplayAlert($"{BluetoothLEService.Device.Name}", $"Unable to disconnect from {BluetoothLEService.Device.Name}.", "OK");
        }
        finally
        {
            Title = "Heart rate";
            HeartRateValue = 0;
            Dataset.HeartRatePatient = 0;
			Timestamp = DateTimeOffset.MinValue;
            IsBusy = false;
            BluetoothLEService.Device?.Dispose();
            BluetoothLEService.Device = null;
            await Shell.Current.GoToAsync("//DevicePage", true);
        }
    }
}
