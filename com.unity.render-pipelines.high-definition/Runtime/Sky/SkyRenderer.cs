namespace UnityEngine.Rendering.HighDefinition
{
    public abstract class SkyRenderer
    {
        public abstract void Build();
        public abstract void Cleanup();
        public abstract void SetRenderTargets(BuiltinSkyParameters builtinParams);
        // renderForCubemap: When rendering into a cube map, no depth buffer is available so user has to make sure not to use depth testing or the depth texture.
        public abstract void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk);
        public abstract bool IsValid();

        protected float GetExposure(SkySettings skySettings, DebugDisplaySettings debugSettings)
        {
            float debugExposure = 0.0f;
            if (debugSettings != null && debugSettings.DebugNeedsExposure())
            {
                debugExposure = debugSettings.data.lightingDebugSettings.debugExposure;
            }
            return ColorUtils.ConvertEV100ToExposure(-(skySettings.exposure.value + debugExposure));
        }

        public static void SetGlobalNeutralSkyData(CommandBuffer cmd)
        {
            cmd.SetGlobalTexture(HDShaderIDs._AirSingleScatteringTexture,     CoreUtils.blackVolumeTexture);
            cmd.SetGlobalTexture(HDShaderIDs._AerosolSingleScatteringTexture, CoreUtils.blackVolumeTexture);
            cmd.SetGlobalTexture(HDShaderIDs._MultipleScatteringTexture,      CoreUtils.blackVolumeTexture);
        }

        public virtual void SetGlobalSkyData(CommandBuffer cmd)
        {
            SetGlobalNeutralSkyData(cmd);
        }
    }
}
