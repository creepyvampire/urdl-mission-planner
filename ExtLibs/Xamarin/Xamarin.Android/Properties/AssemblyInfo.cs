using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Xamarin.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("URDL")]
[assembly: AssemblyProduct("Xamarin.Android")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]

// To prevent the screen from turning off during a mission
[assembly: UsesPermission(Android.Manifest.Permission.WakeLock)]

// For accessing the phone's GPS and external GPS data
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]

// For connecting to devices via Bluetooth or Bluetooth LE
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothAdmin)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothScan)]
[assembly: UsesPermission(Android.Manifest.Permission.BluetoothConnect)]

// --- ESSENTIAL FOR USB CONNECTION ---

// This declares that your app can use USB devices (Host Mode/OTG)
// It corresponds to the <uses-feature> tag in the manifest.
[assembly: UsesFeature("android.hardware.usb.host")]
