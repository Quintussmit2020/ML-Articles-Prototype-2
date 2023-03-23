AAL-2445 - Fixed IsInitialized() method returning incorrect values resulting in incorrect engine startup procedure
AAL-2452 - Update versions and logs for release

Version 3.2.3
^^^============

AAL-2430 - Fixed start/stop calls in the MFCC analysis
AAL-2417 - Provide a method to get the Plugin mode
AAL-2395 - fixed missing script in example
AAL-2422 - Changed the way property definitions are registered
AAL-2420 - Component Gizmo no longer allows negative scaling
AAL-2164 - Fix auto play on awake for ambisonic sources
AAL-2416 - Fixed MFCC variable name
AAL-2343 - Removed no MLListener present warning from project validator
AAL-2359 - Removed listener prefab option
AAL-2399 - Removed MLListener proxy component (Replaced by the MLListener auto-create functionality)
AAL-2326 - Add support for buffer size changes from Unity
AAL-2373 - Remove OS Offload option from MLListener component and make it accessible from the global scriptable object only
AAL-2335 - We now create/destroy plugin between editor play modes and calling d'tor with it
AAL-2327 - Added native plugin logs to the Unity console
AAL-2353 - Added option to enable source radiation gizmos and set their scale and alpha values
AAL-2340 - Updated component question mark links to public documentation
AAL-2346 - Improve performance of User Voice Detection algo
AAL-2382 - Update versions and logs for release

Version 3.2.2
^^^============

=======
AUDIO-5030 - Spatializer Stability and Performance Improvements
AAL-2373 - Fix Dispersion playing back Headlocked
AAL-2332 - Fix for source radiation Gizmo not rendering in the editor
AAL-2218 - Updated samples
AAL-2269 - Remove GC allocation in MLListener OnAudioFilterRead callback
AAL-2117 - Setup MLListener to operate automatically without the proxy component
AAL-2334 - Updated version and description for NPM package

Version 3.2.1  Release Candidate R5
^^^============

AAL-2238 - Fix / Re-enable Acoustic Map Enable
AAL-2237 - Set Billboard radiation default property to true
AAL-2314 - Fix links to documentation on readme.md
AAL-2305 - Workaround for Unity's bug (GizmoManager)
AAL-2296 - Adding error in log when needed objects are disabled
AAL-2217 - Only render MLPointSource Gizmos if their audio source is playing or is not virtual
AAL-2275 - Fixing BnR build issues
AAL-2257 - Fixed issue accessing methods from SDK/ZI
AAL-2251 - Resend point source properties when specialize property is toggled
AAL-2192 - Version bump and commenting out mapping for now

Version 3.2.0  Release Candidate R4
^^^============

AAL-2240 - Unity B4 support for both Intel/M1
AAL-2197 - Small UI changes
AAL-2201 - Removed forced AudioSource changes
AAL-2214 - Fixed MLListener error on domain reload
AAL-2215 - Fixed Warnings about mac libs when building android app
AAL-2159 - Fixed null reference error in MLPointSource when MLListener is not present
AAL-2071 - Fixed erroneous debug log messages with MLListener, MLPointSource and MLDebugInfo components
AAL-2193 - Remove unused .meta file
AAL-2194 - Renamed Scriptable Object to make sure it's known it belongs to MSA
AAL-2206 - Remove hard dependency on SDK
AAL-2161 - Route audio into ML Unity SDK

Version 3.1.4
^^^===========

AAL-2163 - Render GL.Lines at the correct position
AAL-1716 - Added programmatic access to NPM package version number
AAL-2156 - Removed FindObjectOfType calls and log if we are trying to us MLDebugInfo if its not present in the scene
AAL-2070 - Fix to GL.Line and use endContextRendering callback to render the Gizmos
AAL-2126 - Update build scripts for release/profiler
AAL-2134 - Fix UI for Acoustic Mapping
AAL-2141 - Fix for missing labels on UI
AAL-2131 - Fix issue with Analysis example script
AAL-2041 - Enable Acoustic Mapping
AAL-2124 - Updated MLAcousticAnalysis component to use our custom properties and editor
AAL-2127 - Support for Unity Pure Android

Version 3.1.3
^^^============


AAL-2090 - Added Unity profiling markers
AAL-2118 - play on awake bug for ambisonics
AAL-2108 - Removed "Is Head Relative" property from the ambisonic source component and stopped passing the listener transform in the native plugin
AAL-2068 - Make MLListener Proxy component available at the correct menu location
AAL-2101 - Check ambi plugin is selected at start up and added example for ambisonic
AAL-2111 - Fixed audio corruption when playing ambisonic and non-ambisonic sources
AAL-2102 - Set obstruction factor to 1.0f when MLListener raycasts linecast is true
AAL-2069 - MLRuntimeGizmoManager now renders gizmos more consistently
AAL-2068 - Rename MLListenerSpringBoard component to MLListenerProxy and made it available in the hierarchy menu
AAL-2055 - Breaking API rename changes

Version 3.1.2
^^^============

AAL-2084 - Enable Ambisonics plugin and audio source component
AAL-2056 - Reset MLListener component properties on constructor
AAL-2067 - Check for null parent missing in collider search (BUG)
AAL-2061 - Obstructions raycasts collide with MLPointSource Parent colliders (BUG)
AAL-2037 - Update examples for NPM package
AAL-2043 - Reduced visibility of non-public functions and properties
AAL-1935 - Improvements to Gizmo visual appearance
AAL-1983 - Added MLDebugLog class and started logging Importer native API call results
AAL-1982 - Added multi editing support to custom property drawers
AAL-1949 - Added MLListener spring board to support additive scenes
AAL-1948 - Fix for MSA components not working properly unless both MLListener and MLPointSource are present
AAL-2003 - Set Obstruction override properties to linear sliders
AAL-1994 - Added missing DefaultDispersion Gain HF property registration
AAL-1998 - Added missing begin change check that was causing components to be dirty on each frame
AAL-1996 - Fix for isBillboard not working in runtime
AAL-1993 - Fix for MLListener calling GC.alloc on every frame
AAL-1918 - Fix raycast requests to resolve indirect obstruction issues
AAL-1965 - Support CPU usage in build scripts
AAL-1947 - Update build scripts to propagate errors
AAL-1920 - Update right click menu options
AAL-1966 - Avoid allocating memory in Point Source
AAL-1848 - Turn on and verify Analysis
AAL-1934 - Employ C# structs in the Unity plugins
AAL-1850 - Enable OS-offload
AAL-1804 - Support for debugging timing
...
Release candidate for ML1 begin work on ML2

Version 3.1.1
^^^============


Deleted Extra check box on MacOS editor
Version 1.2.37b
^^^===============