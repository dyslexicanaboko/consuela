; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Consuela"
#define MyAppVersion "1.3.0.2"
#define MyAppPublisher "Dyslexicanaboko"
#define MyAppURL "https://github.com/dyslexicanaboko/consuela/"
#define MyAppExeName "Consuela.Service.exe"
#define MyAppShortcut "Consuela.url"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{673FED75-985B-48F3-BED9-103A447EA60E}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename={#MyAppName}Setup_{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

; These File URI are for your local windows system
[Files]
Source: "C:\Dev\GitHub\consuela\Consuela.Service\bin\Release\net6.0\publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Dev\GitHub\consuela\Consuela.Service\bin\Release\net6.0\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Dev\GitHub\consuela\ConsuelaInnoSetup\{#MyAppShortcut}"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppShortcut}"
Name: "{group}\{#MyAppName} on GitHub"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[run]
Filename: {sys}\sc.exe; Parameters: "create {#MyAppName} start=auto binPath=""{app}\{#MyAppExeName}"""; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "start {#MyAppName}"; Flags: runhidden

[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop {#MyAppName}"; RunOnceId: "DelService"; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "delete {#MyAppName}"; RunOnceId: "DelService"; Flags: runhidden

[UninstallDelete]
Type: files; Name: "{app}\api-ms-win-core-console-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-console-l1-2-0.dll"
Type: files; Name: "{app}\api-ms-win-core-datetime-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-debug-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-errorhandling-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-fibers-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-file-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-file-l1-2-0.dll"
Type: files; Name: "{app}\api-ms-win-core-file-l2-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-handle-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-heap-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-interlocked-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-libraryloader-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-localization-l1-2-0.dll"
Type: files; Name: "{app}\api-ms-win-core-memory-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-namedpipe-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-processenvironment-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-processthreads-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-processthreads-l1-1-1.dll"
Type: files; Name: "{app}\api-ms-win-core-profile-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-rtlsupport-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-string-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-synch-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-synch-l1-2-0.dll"
Type: files; Name: "{app}\api-ms-win-core-sysinfo-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-timezone-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-core-util-l1-1-0.dll"
Type: files; Name: "{app}\API-MS-Win-core-xstate-l2-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-conio-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-convert-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-environment-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-filesystem-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-heap-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-locale-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-math-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-multibyte-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-private-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-process-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-runtime-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-stdio-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-string-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-time-l1-1-0.dll"
Type: files; Name: "{app}\api-ms-win-crt-utility-l1-1-0.dll"
Type: files; Name: "{app}\appsettings.Development.json"
Type: files; Name: "{app}\aspnetcorev2_inprocess.dll"
Type: files; Name: "{app}\cat"
Type: files; Name: "{app}\clretwrc.dll"
Type: files; Name: "{app}\clrjit.dll"
Type: files; Name: "{app}\Consuela.Entity.dll"
Type: files; Name: "{app}\Consuela.Entity.pdb"
Type: files; Name: "{app}\Consuela.Lib.dll"
Type: files; Name: "{app}\Consuela.Lib.pdb"
Type: files; Name: "{app}\Consuela.Service.deps.json"
Type: files; Name: "{app}\Consuela.Service.dll"
Type: files; Name: "{app}\Consuela.Service.exe"
Type: files; Name: "{app}\Consuela.Service.pdb"
Type: files; Name: "{app}\Consuela.Service.runtimeconfig.json"
Type: files; Name: "{app}\coreclr.dll"
Type: files; Name: "{app}\createdump.exe"
Type: files; Name: "{app}\dbgshim.dll"
Type: files; Name: "{app}\EPPlus.dll"
Type: files; Name: "{app}\EPPlus.Interfaces.dll"
Type: files; Name: "{app}\EPPlus.System.Drawing.dll"
Type: files; Name: "{app}\hostfxr.dll"
Type: files; Name: "{app}\hostpolicy.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Antiforgery.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authentication.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authentication.Cookies.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authentication.Core.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authentication.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authentication.OAuth.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authorization.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Authorization.Policy.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Components.Authorization.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Components.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Components.Forms.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Components.Server.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Components.Web.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Connections.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.CookiePolicy.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Cors.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Cryptography.Internal.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Cryptography.KeyDerivation.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.DataProtection.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.DataProtection.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.DataProtection.Extensions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Diagnostics.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Diagnostics.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Diagnostics.HealthChecks.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.HostFiltering.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Hosting.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Hosting.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Hosting.Server.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Html.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.Connections.Common.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.Connections.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.Extensions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.Features.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Http.Results.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.HttpLogging.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.HttpOverrides.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.HttpsPolicy.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Identity.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Localization.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Localization.Routing.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Metadata.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.ApiExplorer.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Core.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Cors.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.DataAnnotations.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Formatters.Json.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Formatters.Xml.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Localization.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.Razor.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.RazorPages.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.TagHelpers.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Mvc.ViewFeatures.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Razor.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Razor.Runtime.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.ResponseCaching.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.ResponseCaching.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.ResponseCompression.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Rewrite.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Routing.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Routing.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.HttpSys.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.IIS.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.IISIntegration.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.Kestrel.Core.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.Kestrel.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.Kestrel.Transport.Quic.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.Session.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.SignalR.Common.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.SignalR.Core.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.SignalR.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.SignalR.Protocols.Json.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.StaticFiles.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.WebSockets.dll"
Type: files; Name: "{app}\Microsoft.AspNetCore.WebUtilities.dll"
Type: files; Name: "{app}\Microsoft.CSharp.dll"
Type: files; Name: "{app}\Microsoft.DiaSymReader.Native.x86.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Caching.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Caching.Memory.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.Binder.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.CommandLine.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.EnvironmentVariables.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.FileExtensions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.Ini.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.Json.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.KeyPerFile.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.UserSecrets.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Configuration.Xml.dll"
Type: files; Name: "{app}\Microsoft.Extensions.DependencyInjection.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.DependencyInjection.dll"
Type: files; Name: "{app}\Microsoft.Extensions.DependencyModel.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Diagnostics.HealthChecks.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Diagnostics.HealthChecks.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Features.dll"
Type: files; Name: "{app}\Microsoft.Extensions.FileProviders.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.FileProviders.Composite.dll"
Type: files; Name: "{app}\Microsoft.Extensions.FileProviders.Embedded.dll"
Type: files; Name: "{app}\Microsoft.Extensions.FileProviders.Physical.dll"
Type: files; Name: "{app}\Microsoft.Extensions.FileSystemGlobbing.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Hosting.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Hosting.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Hosting.WindowsServices.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Http.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Identity.Core.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Identity.Stores.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Localization.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Localization.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.Abstractions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.Configuration.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.Console.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.Debug.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.EventLog.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.EventSource.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Logging.TraceSource.dll"
Type: files; Name: "{app}\Microsoft.Extensions.ObjectPool.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Options.ConfigurationExtensions.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Options.DataAnnotations.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Options.dll"
Type: files; Name: "{app}\Microsoft.Extensions.Primitives.dll"
Type: files; Name: "{app}\Microsoft.Extensions.WebEncoders.dll"
Type: files; Name: "{app}\Microsoft.IO.RecyclableMemoryStream.dll"
Type: files; Name: "{app}\Microsoft.JSInterop.dll"
Type: files; Name: "{app}\Microsoft.Net.Http.Headers.dll"
Type: files; Name: "{app}\Microsoft.VisualBasic.Core.dll"
Type: files; Name: "{app}\Microsoft.VisualBasic.dll"
Type: files; Name: "{app}\Microsoft.Win32.Primitives.dll"
Type: files; Name: "{app}\Microsoft.Win32.Registry.dll"
Type: files; Name: "{app}\Microsoft.Win32.SystemEvents.dll"
Type: files; Name: "{app}\mscordaccore.dll"
Type: files; Name: "{app}\mscordaccore_x86_x86_6.0.722.32202.dll"
Type: files; Name: "{app}\mscordbi.dll"
Type: files; Name: "{app}\mscorlib.dll"
Type: files; Name: "{app}\mscorrc.dll"
Type: files; Name: "{app}\msquic.dll"
Type: files; Name: "{app}\netstandard.dll"
Type: files; Name: "{app}\Newtonsoft.Json.dll"
Type: files; Name: "{app}\Serilog.dll"
Type: files; Name: "{app}\Serilog.Extensions.Hosting.dll"
Type: files; Name: "{app}\Serilog.Extensions.Logging.dll"
Type: files; Name: "{app}\Serilog.Formatting.Compact.dll"
Type: files; Name: "{app}\Serilog.Settings.Configuration.dll"
Type: files; Name: "{app}\Serilog.Sinks.Console.dll"
Type: files; Name: "{app}\Serilog.Sinks.File.dll"
Type: files; Name: "{app}\Serilog.Sinks.PeriodicBatching.dll"
Type: files; Name: "{app}\Serilog.Sinks.Seq.dll"
Type: files; Name: "{app}\System.AppContext.dll"
Type: files; Name: "{app}\System.Buffers.dll"
Type: files; Name: "{app}\System.Collections.Concurrent.dll"
Type: files; Name: "{app}\System.Collections.dll"
Type: files; Name: "{app}\System.Collections.Immutable.dll"
Type: files; Name: "{app}\System.Collections.NonGeneric.dll"
Type: files; Name: "{app}\System.Collections.Specialized.dll"
Type: files; Name: "{app}\System.ComponentModel.Annotations.dll"
Type: files; Name: "{app}\System.ComponentModel.DataAnnotations.dll"
Type: files; Name: "{app}\System.ComponentModel.dll"
Type: files; Name: "{app}\System.ComponentModel.EventBasedAsync.dll"
Type: files; Name: "{app}\System.ComponentModel.Primitives.dll"
Type: files; Name: "{app}\System.ComponentModel.TypeConverter.dll"
Type: files; Name: "{app}\System.Configuration.dll"
Type: files; Name: "{app}\System.Console.dll"
Type: files; Name: "{app}\System.Core.dll"
Type: files; Name: "{app}\System.Data.Common.dll"
Type: files; Name: "{app}\System.Data.DataSetExtensions.dll"
Type: files; Name: "{app}\System.Data.dll"
Type: files; Name: "{app}\System.Diagnostics.Contracts.dll"
Type: files; Name: "{app}\System.Diagnostics.Debug.dll"
Type: files; Name: "{app}\System.Diagnostics.DiagnosticSource.dll"
Type: files; Name: "{app}\System.Diagnostics.EventLog.dll"
Type: files; Name: "{app}\System.Diagnostics.EventLog.Messages.dll"
Type: files; Name: "{app}\System.Diagnostics.FileVersionInfo.dll"
Type: files; Name: "{app}\System.Diagnostics.Process.dll"
Type: files; Name: "{app}\System.Diagnostics.StackTrace.dll"
Type: files; Name: "{app}\System.Diagnostics.TextWriterTraceListener.dll"
Type: files; Name: "{app}\System.Diagnostics.Tools.dll"
Type: files; Name: "{app}\System.Diagnostics.TraceSource.dll"
Type: files; Name: "{app}\System.Diagnostics.Tracing.dll"
Type: files; Name: "{app}\System.dll"
Type: files; Name: "{app}\System.Drawing.Common.dll"
Type: files; Name: "{app}\System.Drawing.dll"
Type: files; Name: "{app}\System.Drawing.Primitives.dll"
Type: files; Name: "{app}\System.Dynamic.Runtime.dll"
Type: files; Name: "{app}\System.Formats.Asn1.dll"
Type: files; Name: "{app}\System.Globalization.Calendars.dll"
Type: files; Name: "{app}\System.Globalization.dll"
Type: files; Name: "{app}\System.Globalization.Extensions.dll"
Type: files; Name: "{app}\System.IO.Compression.Brotli.dll"
Type: files; Name: "{app}\System.IO.Compression.dll"
Type: files; Name: "{app}\System.IO.Compression.FileSystem.dll"
Type: files; Name: "{app}\System.IO.Compression.Native.dll"
Type: files; Name: "{app}\System.IO.Compression.ZipFile.dll"
Type: files; Name: "{app}\System.IO.dll"
Type: files; Name: "{app}\System.IO.FileSystem.AccessControl.dll"
Type: files; Name: "{app}\System.IO.FileSystem.dll"
Type: files; Name: "{app}\System.IO.FileSystem.DriveInfo.dll"
Type: files; Name: "{app}\System.IO.FileSystem.Primitives.dll"
Type: files; Name: "{app}\System.IO.FileSystem.Watcher.dll"
Type: files; Name: "{app}\System.IO.IsolatedStorage.dll"
Type: files; Name: "{app}\System.IO.MemoryMappedFiles.dll"
Type: files; Name: "{app}\System.IO.Pipelines.dll"
Type: files; Name: "{app}\System.IO.Pipes.AccessControl.dll"
Type: files; Name: "{app}\System.IO.Pipes.dll"
Type: files; Name: "{app}\System.IO.UnmanagedMemoryStream.dll"
Type: files; Name: "{app}\System.Linq.dll"
Type: files; Name: "{app}\System.Linq.Expressions.dll"
Type: files; Name: "{app}\System.Linq.Parallel.dll"
Type: files; Name: "{app}\System.Linq.Queryable.dll"
Type: files; Name: "{app}\System.Memory.dll"
Type: files; Name: "{app}\System.Net.dll"
Type: files; Name: "{app}\System.Net.Http.dll"
Type: files; Name: "{app}\System.Net.Http.Json.dll"
Type: files; Name: "{app}\System.Net.HttpListener.dll"
Type: files; Name: "{app}\System.Net.Mail.dll"
Type: files; Name: "{app}\System.Net.NameResolution.dll"
Type: files; Name: "{app}\System.Net.NetworkInformation.dll"
Type: files; Name: "{app}\System.Net.Ping.dll"
Type: files; Name: "{app}\System.Net.Primitives.dll"
Type: files; Name: "{app}\System.Net.Quic.dll"
Type: files; Name: "{app}\System.Net.Requests.dll"
Type: files; Name: "{app}\System.Net.Security.dll"
Type: files; Name: "{app}\System.Net.ServicePoint.dll"
Type: files; Name: "{app}\System.Net.Sockets.dll"
Type: files; Name: "{app}\System.Net.WebClient.dll"
Type: files; Name: "{app}\System.Net.WebHeaderCollection.dll"
Type: files; Name: "{app}\System.Net.WebProxy.dll"
Type: files; Name: "{app}\System.Net.WebSockets.Client.dll"
Type: files; Name: "{app}\System.Net.WebSockets.dll"
Type: files; Name: "{app}\System.Numerics.dll"
Type: files; Name: "{app}\System.Numerics.Vectors.dll"
Type: files; Name: "{app}\System.ObjectModel.dll"
Type: files; Name: "{app}\System.Private.CoreLib.dll"
Type: files; Name: "{app}\System.Private.DataContractSerialization.dll"
Type: files; Name: "{app}\System.Private.Uri.dll"
Type: files; Name: "{app}\System.Private.Xml.dll"
Type: files; Name: "{app}\System.Private.Xml.Linq.dll"
Type: files; Name: "{app}\System.Reflection.DispatchProxy.dll"
Type: files; Name: "{app}\System.Reflection.dll"
Type: files; Name: "{app}\System.Reflection.Emit.dll"
Type: files; Name: "{app}\System.Reflection.Emit.ILGeneration.dll"
Type: files; Name: "{app}\System.Reflection.Emit.Lightweight.dll"
Type: files; Name: "{app}\System.Reflection.Extensions.dll"
Type: files; Name: "{app}\System.Reflection.Metadata.dll"
Type: files; Name: "{app}\System.Reflection.Primitives.dll"
Type: files; Name: "{app}\System.Reflection.TypeExtensions.dll"
Type: files; Name: "{app}\System.Resources.Reader.dll"
Type: files; Name: "{app}\System.Resources.ResourceManager.dll"
Type: files; Name: "{app}\System.Resources.Writer.dll"
Type: files; Name: "{app}\System.Runtime.CompilerServices.Unsafe.dll"
Type: files; Name: "{app}\System.Runtime.CompilerServices.VisualC.dll"
Type: files; Name: "{app}\System.Runtime.dll"
Type: files; Name: "{app}\System.Runtime.Extensions.dll"
Type: files; Name: "{app}\System.Runtime.Handles.dll"
Type: files; Name: "{app}\System.Runtime.InteropServices.dll"
Type: files; Name: "{app}\System.Runtime.InteropServices.RuntimeInformation.dll"
Type: files; Name: "{app}\System.Runtime.Intrinsics.dll"
Type: files; Name: "{app}\System.Runtime.Loader.dll"
Type: files; Name: "{app}\System.Runtime.Numerics.dll"
Type: files; Name: "{app}\System.Runtime.Serialization.dll"
Type: files; Name: "{app}\System.Runtime.Serialization.Formatters.dll"
Type: files; Name: "{app}\System.Runtime.Serialization.Json.dll"
Type: files; Name: "{app}\System.Runtime.Serialization.Primitives.dll"
Type: files; Name: "{app}\System.Runtime.Serialization.Xml.dll"
Type: files; Name: "{app}\System.Security.AccessControl.dll"
Type: files; Name: "{app}\System.Security.Claims.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Algorithms.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Cng.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Csp.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Encoding.dll"
Type: files; Name: "{app}\System.Security.Cryptography.OpenSsl.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Pkcs.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Primitives.dll"
Type: files; Name: "{app}\System.Security.Cryptography.X509Certificates.dll"
Type: files; Name: "{app}\System.Security.Cryptography.Xml.dll"
Type: files; Name: "{app}\System.Security.dll"
Type: files; Name: "{app}\System.Security.Principal.dll"
Type: files; Name: "{app}\System.Security.Principal.Windows.dll"
Type: files; Name: "{app}\System.Security.SecureString.dll"
Type: files; Name: "{app}\System.ServiceModel.Web.dll"
Type: files; Name: "{app}\System.ServiceProcess.dll"
Type: files; Name: "{app}\System.ServiceProcess.ServiceController.dll"
Type: files; Name: "{app}\System.Text.Encoding.CodePages.dll"
Type: files; Name: "{app}\System.Text.Encoding.dll"
Type: files; Name: "{app}\System.Text.Encoding.Extensions.dll"
Type: files; Name: "{app}\System.Text.Encodings.Web.dll"
Type: files; Name: "{app}\System.Text.Json.dll"
Type: files; Name: "{app}\System.Text.RegularExpressions.dll"
Type: files; Name: "{app}\System.Threading.Channels.dll"
Type: files; Name: "{app}\System.Threading.dll"
Type: files; Name: "{app}\System.Threading.Overlapped.dll"
Type: files; Name: "{app}\System.Threading.Tasks.Dataflow.dll"
Type: files; Name: "{app}\System.Threading.Tasks.dll"
Type: files; Name: "{app}\System.Threading.Tasks.Extensions.dll"
Type: files; Name: "{app}\System.Threading.Tasks.Parallel.dll"
Type: files; Name: "{app}\System.Threading.Thread.dll"
Type: files; Name: "{app}\System.Threading.ThreadPool.dll"
Type: files; Name: "{app}\System.Threading.Timer.dll"
Type: files; Name: "{app}\System.Transactions.dll"
Type: files; Name: "{app}\System.Transactions.Local.dll"
Type: files; Name: "{app}\System.ValueTuple.dll"
Type: files; Name: "{app}\System.Web.dll"
Type: files; Name: "{app}\System.Web.HttpUtility.dll"
Type: files; Name: "{app}\System.Windows.dll"
Type: files; Name: "{app}\System.Xml.dll"
Type: files; Name: "{app}\System.Xml.Linq.dll"
Type: files; Name: "{app}\System.Xml.ReaderWriter.dll"
Type: files; Name: "{app}\System.Xml.Serialization.dll"
Type: files; Name: "{app}\System.Xml.XDocument.dll"
Type: files; Name: "{app}\System.Xml.XmlDocument.dll"
Type: files; Name: "{app}\System.Xml.XmlSerializer.dll"
Type: files; Name: "{app}\System.Xml.XPath.dll"
Type: files; Name: "{app}\System.Xml.XPath.XDocument.dll"
Type: files; Name: "{app}\ucrtbase.dll"
Type: files; Name: "{app}\web.config"
Type: files; Name: "{app}\WindowsBase.dll"
Type: files; Name: "{app}\wwwroot\*"
Type: dirifempty; Name: "{app}\wwwroot"
