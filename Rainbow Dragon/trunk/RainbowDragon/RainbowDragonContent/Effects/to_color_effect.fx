sampler s0;
texture lightMask;
sampler lightSampler = sampler_state{Texture = lightMask;};

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	float4 lightColor = tex2D(lightSampler, coords);
	
	if(lightColor.r == 0 && lightColor.g ==0 &&lightColor.b == 0)
	color.rgba = float4(0,0,0,0);
	
	return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
