//-----------------------------------------------------------------------------
// Configuration
//-----------------------------------------------------------------------------

namespace UnityEngine.Rendering.HighDefinition
{
    [GenerateHLSL(PackingRules.Exact)]
    public enum ShaderOptions
    {
        CameraRelativeRendering = 1, // Rendering sets the origin of the world to the position of the primary (scene view) camera
        PreExposition = 1,
        PrecomputedAtmosphericAttenuation = 0, // Precomputes atmospheric attenuation for the directional light on the CPU, which makes it independent from the fragment's position, which is faster but wrong
#if ENABLE_RAYTRACING
        Raytracing = 1,
#else
        Raytracing = 0,
#endif
#if ENABLE_VR
        XrMaxViews = 8, // Used for single-pass rendering (with fast path in vertex shader code when forced to 2)
#else
        XrMaxViews = 1,
#endif
    };

    // Note: #define can't be use in include file in C# so we chose this way to configure both C# and hlsl
    // Changing a value in this enum Config here require to regenerate the hlsl include and recompile C# and shaders
    public class ShaderConfig
    {
        public static int s_CameraRelativeRendering = (int)ShaderOptions.CameraRelativeRendering;
        public static int s_PreExposition = (int)ShaderOptions.PreExposition;

        // XRTODO: shader constants using this macro could be switched to StructuredBuffer instead of fixed-size array (if performance is similar)
        public static int s_XrMaxViews = (int)ShaderOptions.XrMaxViews;

        public static int s_PrecomputedAtmosphericAttenuation = (int)ShaderOptions.PrecomputedAtmosphericAttenuation;
    }
}
