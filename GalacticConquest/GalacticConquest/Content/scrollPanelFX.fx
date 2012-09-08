float2 fShieldFacing;  
float fShieldSize;  
float fShieldInnerRadius;  
 
sampler TextureSampler : register(s0);  
 
float4 PixelShaderFunction(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0  
{  
    float4 output = tex2D(TextureSampler, texCoord) * color;  
 
    float2 center = float2(0.5, 0.5);  
    float2 pixelFacing = normalize(texCoord - center);  
    float dist = distance(texCoord, center);  
      
    float dFacing = dot(pixelFacing, fShieldFacing);  
    float power = step(0, dFacing);  
    power *= step(fShieldInnerRadius, dist);  
    output.a *= pow(power * dFacing, 1 / fShieldSize);  
 
    return output;  
}  
 
technique Default  
{  
    pass p0  
    {  
        PixelShader = compile ps_2_0 PixelShaderFunction();  
    }  
}  