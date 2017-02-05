# RichTextEditor
Xamarin Cross-Platform PCL for a Rich Text Editor with style buttons and html import/export.

<h2>Setup</h2>
For Xamarin.Forms cross-platform projects, add the following line to `App.xaml.cs` or `App.cs`:<br>
```c#
DependencyService.Get<RegisterEditor>(); // in OnStart method
```

For use in a Xamarin.iOS app, add the following line to `UIApplicationDelegate.cs`:<br>
```c#
RegisterEditorIOS register = new RegisterEditorIOS(); // Insert after Forms.Init() in FinishedLaunching
```

For Android, add in the following to the `OnCreate` method of `MainActivity.cs`:<br>
```c#
RegisterEditorDroid register = new RegisterEditorDroid();
