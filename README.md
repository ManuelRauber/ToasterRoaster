Toaster Roaster
==============

ToasterRoaster is a game developed within the course "Games & Gaming" at the cooperative state university Baden-Württemberg Karlsruhe.

The game is a cross-plattform game, fully written in C# and the WaveEngine. It will support Desktop, Windows Phone and Android.

You can find out more at: http://tinf11games.wordpress.com/category/projekte/toaster-roaster/ (German/English Blog)

Project structure 
=================

The project contains two types of projects:
  
  * Launcher project: Platform specific project to start up the game on its target platform
  * Game project: Platform independent project (PCL) containing the game logic

The solution contains currently the following projects:

  * ToasterRoaster.Desktop: Desktop launcher project
  * ToasterRoaster.WindowsPhone: Windows phone launcher project
  * ToasterRoaster.Android: Android launcher project
  * ToasterRoaster.Game.*: Game logic projects for the specific platform to use the correct WaveEngine references. The projects share code.

Project preparation
==============

Prerequisites
-------------
  * Visual Studio 2013
  * WaveEngine (http://waveengine.net)
  * Xamarin.Android (http://xamarin.com) (only needed, if you want to compile the Android 4.4)
  * Windows Phone SDK (http://dev.windowsphone.com/en-us/downloadsdk) (only needed, if you want to compile for Windows Phone)
  * HyperV (if you want to use the Windows Phone Emulator; will be installed with Windows Phone SDK)
  
Android preparation
-------------------

If you want to compile for Android you need to install Xamarin.Android. After the installation, start the Android SDK Manager and download the following packages for Android 4.4.2 (API 19):
  
  * SDK Platform
  * ARM EABI v7a System Image
  
After the download, open the Android AVD Manager and create a new virtual device with the following setup:

  * Device: Use the one you want, should have at least 1 GB RAM
  * Target: Android 4.4.2 - API 19
  * CPU/ABI: ARM (armeabi-v7a)
  * Skin: HVGA
  * Use Host GPU: yes
  
You now can compile for Android and launch the Emulator for testing purposes.